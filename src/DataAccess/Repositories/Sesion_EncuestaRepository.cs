using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Sesion_EncuestaRepository : ISesion_EncuestaRepository
    {
        private readonly IDbConnection _connection;

        public Sesion_EncuestaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Sesion_Encuesta>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdSesion", id);
            try
            {
                return await _connection.QueryAsync<Sesion_Encuesta>(
                    "sp_Sesion_Encuesta_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Sesion_Encuesta entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdSesion", entity.iIdSesion);
            parameters.Add("@iIdEncuesta", entity.iIdEncuesta);
            parameters.Add("@cUsuarioRespuesta", entity.cUsuarioRespuesta);
            parameters.Add("@fFechaInicio", entity.fFechaInicio);
            parameters.Add("@fFechaCompletado", entity.fFechaCompletado);
            parameters.Add("@cEstado", entity.cEstado);
            parameters.Add("@cIPAddress", entity.cIPAddress);
            parameters.Add("@cCodigoConfirmacion", entity.cCodigoConfirmacion);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Sesion_Encuesta_Mantenimiento",
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
