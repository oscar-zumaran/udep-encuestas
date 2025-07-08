using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Log_AuditoriaRepository : ILog_AuditoriaRepository
    {
        private readonly IDbConnection _connection;

        public Log_AuditoriaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Log_Auditoria>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdLogAuditoria", id);
            try
            {
                return await _connection.QueryAsync<Log_Auditoria>(
                    "sp_Log_Auditoria_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Log_Auditoria entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdLogAuditoria", entity.iIdLogAuditoria);
            parameters.Add("@cTablaAfectada", entity.cTablaAfectada);
            parameters.Add("@iIdRegistroAfectado", entity.iIdRegistroAfectado);
            parameters.Add("@cOperacion", entity.cOperacion);
            parameters.Add("@cUsuario", entity.cUsuario);
            parameters.Add("@fTimestamp", entity.fTimestamp);
            parameters.Add("@cValoresAnteriores", entity.cValoresAnteriores);
            parameters.Add("@cValoresNuevos", entity.cValoresNuevos);
            parameters.Add("@cIPAddress", entity.cIPAddress);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Log_Auditoria_Mantenimiento",
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
