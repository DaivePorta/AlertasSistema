using AlertasUnicorn.Application.Services;
using AlertasUnicorn.Domain.Repositories;
using AlertasUnicorn.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AlertasUnicorn.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            LoadAppSettings();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            try
            {
                Log.Information("Application Starting Up");

                using IHost host = Host.CreateDefaultBuilder(args).UseSerilog()
                    .ConfigureServices((_, services) =>
                        services.AddTransient<IAlertaService, AlertaService>()
                        .AddTransient<IUnitOfWork, UnitOfWork>()
                        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                    ).Build();

                ExecuteAlertaService(host.Services);

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to start correctly");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        static void LoadAppSettings()
        {
            var serviceCollection = new ServiceCollection();
            IConfiguration configuration;
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json")
                .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
        }

        static T GetServiceProvider<T>(IServiceProvider serviceProvider)
        {
            using IServiceScope serviceScope = serviceProvider.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            return provider.GetRequiredService<T>();
        }

        static void ExecuteAlertaService(IServiceProvider serviceProvider)
        {
            try
            {
                IAlertaService service = GetServiceProvider<IAlertaService>(serviceProvider);
                Log.Warning("Inicio de ejecucucion de la generacion de alertas de contratos");
                service.GenerarAlertasContratos();
                Log.Warning("Fin de ejecucucion de la generacion de alertas de contratos");
            }
            catch (Exception ex)
            {
                Log.Error($"Exception: { ex.Message }");
                Log.Error($"StackTrace: { ex.StackTrace}");
            }
        }
    }
}
