using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Pregunta_EncuestaRepository : IPregunta_EncuestaRepository
    {
        private readonly IDbConnection _connection;

        public Pregunta_EncuestaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Pregunta_Encuesta>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdPreguntaEncuesta", id);
            try
            {
                return await _connection.QueryAsync<Pregunta_Encuesta>(
                    "sp_Pregunta_Encuesta_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Pregunta_Encuesta entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdPreguntaEncuesta", entity.iIdPreguntaEncuesta);
            parameters.Add("@iIdEncuesta", entity.iIdEncuesta);
            parameters.Add("@iIdPregunta", entity.iIdPregunta);
            parameters.Add("@cTipoPregunta", entity.cTipoPregunta);
            parameters.Add("@cDescripcion", entity.cDescripcion);
            parameters.Add("@bVariasRespuestas", entity.bVariasRespuestas);
            parameters.Add("@bRespuestaLarga", entity.bRespuestaLarga);
            parameters.Add("@iNivelCalificacion", entity.iNivelCalificacion);
            parameters.Add("@bObligatoria", entity.bObligatoria);
            parameters.Add("@iOrden", entity.iOrden);
            parameters.Add("@bActiva", entity.bActiva);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Pregunta_Encuesta_Mantenimiento",
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
