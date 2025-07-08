using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Opcion_Pregunta_EncuestaRepository : IOpcion_Pregunta_EncuestaRepository
    {
        private readonly IDbConnection _connection;

        public Opcion_Pregunta_EncuestaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Opcion_Pregunta_Encuesta>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdOpcionPreguntaEncuesta", id);
            try
            {
                return await _connection.QueryAsync<Opcion_Pregunta_Encuesta>(
                    "sp_Opcion_Pregunta_Encuesta_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Opcion_Pregunta_Encuesta entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdOpcionPreguntaEncuesta", entity.iIdOpcionPreguntaEncuesta);
            parameters.Add("@iIdEncuesta", entity.iIdEncuesta);
            parameters.Add("@iIdPreguntaEncuesta", entity.iIdPreguntaEncuesta);
            parameters.Add("@iIdPregunta", entity.iIdPregunta);
            parameters.Add("@iIdOpcionPregunta", entity.iIdOpcionPregunta);
            parameters.Add("@cDescripcionOpcion", entity.cDescripcionOpcion);
            parameters.Add("@bActiva", entity.bActiva);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Opcion_Pregunta_Encuesta_Mantenimiento",
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
