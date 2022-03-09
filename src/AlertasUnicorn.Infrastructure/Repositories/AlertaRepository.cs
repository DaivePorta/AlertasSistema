using AlertasUnicorn.Domain.Entities;
using AlertasUnicorn.Domain.Repositories;
using AlertasUnicorn.Infrastructure.Providers;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AlertasUnicorn.Infrastructure.Repositories
{
    public class AlertaRepository : BaseRepository<Alerta>, IAlertaRepository
    {
        public AlertaRepository(ConnectionProvider connection) : base(connection) { }

        public override async Task Insert(Alerta entity)
        {
            using var connection = _connection.GetConnection();
            string query = @"INSERT INTO ftvaler
                                   (FTVALER_USUARIO_DESTINO
                                  ,FTVALER_USUARIO_ORIGEN
                                  ,FTVALER_MODULO
                                  ,FTVALER_CONTENIDO
                                  ,FTVALER_URL)
                             VALUES
                                   (@UsuarioDestino
                                   ,@UsuarioOrigen
                                   ,@Modulo
                                   ,@Contenido
                                   ,@Url)";
            var parameters = new
            {
                entity.UsuarioDestino,
                entity.UsuarioOrigen,
                entity.Modulo,
                entity.Contenido,
                entity.Url
            };
            await connection.ExecuteAsync(query, parameters);
        }

        public override Task AddRange(IEnumerable<Alerta> entities)
        {
            throw new NotImplementedException();
        }

        public override Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<Alerta>> GetAll()
        {
            using var connection = _connection.GetConnection();
            string query = @"SELECT  
                                    FTVALER_ID AS Id
                                    ,FTVALER_USUARIO_DESTINO AS UsuarioDestino
                                    ,FTVALER_USUARIO_ORIGEN AS UsuarioOrigen
                                    ,FTVALER_MODULO AS Modulo
                                    ,FTVALER_CONTENIDO AS Contenido
                                    ,FTVALER_URL AS Url
                                    ,FTVALER_FECHA AS Fecha
                                    ,FTVALER_ACTIVO AS Activo
                                FROM ftvaler";
            return await connection.QueryAsync<Alerta>(query);
        }

        public override async Task<Alerta> GetById(int id)
        {
            using (var connection = _connection.GetConnection())
            {
                string query = @"SELECT 
                                    FTVALER_ID AS Id
                                    ,FTVALER_USUARIO_DESTINO AS UsuarioDestino
                                    ,FTVALER_USUARIO_ORIGEN AS UsuarioOrigen
                                    ,FTVALER_MODULO AS Modulo
                                    ,FTVALER_CONTENIDO AS Contenido
                                    ,FTVALER_URL AS Url
                                    ,FTVALER_FECHA AS Fecha
                                    ,FTVALER_ACTIVO AS Activo
                                FROM ftvaler
                                WHERE FTVALER_ID = @Id";

                var parameters = new
                {
                    Id = id
                };

                return await connection.QueryFirstOrDefaultAsync<Alerta>(query, parameters);
            }
        }

        public override Task RemoveRange(IEnumerable<Alerta> entities)
        {
            throw new NotImplementedException();
        }

        public override Task Update(Alerta entity)
        {
            throw new NotImplementedException();
        }

        public async Task GenerarAlertasContratos()
        {
            using var connection = _connection.GetConnection();
            await connection.QueryFirstOrDefaultAsync("SpAlertas_GenerarAlertaContratos", commandType: CommandType.StoredProcedure);
        }
    }
}
