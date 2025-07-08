using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Respuesta_CalificacionRepository : IRespuesta_CalificacionRepository
    {
        private readonly IDbConnection _connection;

        public Respuesta_CalificacionRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Respuesta_Calificacion>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdRespuesta", id);
            try
            {
                return await _connection.QueryAsync<Respuesta_Calificacion>(
                    "sp_Respuesta_Calificacion_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Respuesta_Calificacion entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdRespuesta", entity.iIdRespuesta);
            parameters.Add("@iIdSesion", entity.iIdSesion);
            parameters.Add("@iIdPreguntaEncuesta", entity.iIdPreguntaEncuesta);
            parameters.Add("@iValorCalificacion", entity.iValorCalificacion);
            parameters.Add("@cUsuarioRespuesta", entity.cUsuarioRespuesta);
            parameters.Add("@fFechaRespuesta", entity.fFechaRespuesta);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Respuesta_Calificacion_Mantenimiento",
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
