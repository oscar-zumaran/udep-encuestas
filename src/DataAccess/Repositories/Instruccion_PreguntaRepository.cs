using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Instruccion_PreguntaRepository : IInstruccion_PreguntaRepository
    {
        private readonly IDbConnection _connection;

        public Instruccion_PreguntaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Instruccion_Pregunta>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdInstruccion", id);
            try
            {
                return await _connection.QueryAsync<Instruccion_Pregunta>(
                    "sp_Instruccion_Pregunta_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Instruccion_Pregunta entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdInstruccion", entity.iIdInstruccion);
            parameters.Add("@iIdPregunta", entity.iIdPregunta);
            parameters.Add("@cDescripcionInstruccion", entity.cDescripcionInstruccion);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Instruccion_Pregunta_Mantenimiento",
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
