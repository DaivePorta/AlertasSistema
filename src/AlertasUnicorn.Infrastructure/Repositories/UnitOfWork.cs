using AlertasUnicorn.Domain.Repositories;
using AlertasUnicorn.Infrastructure.Providers;
using Microsoft.Extensions.Configuration;

namespace AlertasUnicorn.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionProvider _connectionProvider;
        private IAlertaRepository _alertaRepository;

        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionProvider = new ConnectionProvider(_configuration.GetConnectionString("BD_NOTARIA_GS"));
        }

        public IAlertaRepository AlertaRepository
        {
            get
            {
                if (_alertaRepository == null)
                    _alertaRepository = new AlertaRepository(_connectionProvider);
                return _alertaRepository;
            }
        }

        public void Dispose()
        {
            if (_connectionProvider != null)
            {
                _connectionProvider.Dispose();
            }
        }
    }
}
