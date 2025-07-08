using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Instruccion_Pregunta_EncuestaRepository : IInstruccion_Pregunta_EncuestaRepository
    {
        private readonly IDbConnection _connection;

        public Instruccion_Pregunta_EncuestaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Instruccion_Pregunta_Encuesta>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdInstruccionPreguntaEncuesta", id);
            try
            {
                return await _connection.QueryAsync<Instruccion_Pregunta_Encuesta>(
                    "sp_Instruccion_Pregunta_Encuesta_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Instruccion_Pregunta_Encuesta entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdInstruccionPreguntaEncuesta", entity.iIdInstruccionPreguntaEncuesta);
            parameters.Add("@iIdEncuesta", entity.iIdEncuesta);
            parameters.Add("@iIdPreguntaEncuesta", entity.iIdPreguntaEncuesta);
            parameters.Add("@iIdPregunta", entity.iIdPregunta);
            parameters.Add("@iIdInstruccionPregunta", entity.iIdInstruccionPregunta);
            parameters.Add("@cDescripcionInstruccion", entity.cDescripcionInstruccion);
            parameters.Add("@bActiva", entity.bActiva);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Instruccion_Pregunta_Encuesta_Mantenimiento",
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
