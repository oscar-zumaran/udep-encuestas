using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Respuesta_LikertRepository : IRespuesta_LikertRepository
    {
        private readonly IDbConnection _connection;

        public Respuesta_LikertRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Respuesta_Likert>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdRespuesta", id);
            try
            {
                return await _connection.QueryAsync<Respuesta_Likert>(
                    "sp_Respuesta_Likert_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Respuesta_Likert entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdRespuesta", entity.iIdRespuesta);
            parameters.Add("@iIdSesion", entity.iIdSesion);
            parameters.Add("@iIdPreguntaEncuesta", entity.iIdPreguntaEncuesta);
            parameters.Add("@iIdOpcionSeleccionada", entity.iIdOpcionSeleccionada);
            parameters.Add("@iIdInstruccion", entity.iIdInstruccion);
            parameters.Add("@cUsuarioRespuesta", entity.cUsuarioRespuesta);
            parameters.Add("@fFechaRespuesta", entity.fFechaRespuesta);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Respuesta_Likert_Mantenimiento",
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
