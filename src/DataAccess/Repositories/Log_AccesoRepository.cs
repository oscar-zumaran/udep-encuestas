using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Log_AccesoRepository : ILog_AccesoRepository
    {
        private readonly IDbConnection _connection;

        public Log_AccesoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Log_Acceso>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdLogAcceso", id);
            try
            {
                return await _connection.QueryAsync<Log_Acceso>(
                    "sp_Log_Acceso_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Log_Acceso entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdLogAcceso", entity.iIdLogAcceso);
            parameters.Add("@cUsuario", entity.cUsuario);
            parameters.Add("@cIPAddress", entity.cIPAddress);
            parameters.Add("@fTimestamp", entity.fTimestamp);
            parameters.Add("@cResultado", entity.cResultado);
            parameters.Add("@cUserAgent", entity.cUserAgent);
            parameters.Add("@cDetallesError", entity.cDetallesError);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Log_Acceso_Mantenimiento",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }
    }
}
