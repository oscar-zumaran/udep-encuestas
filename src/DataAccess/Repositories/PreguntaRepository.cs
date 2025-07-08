using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class PreguntaRepository : IPreguntaRepository
    {
        private readonly IDbConnection _connection;

        public PreguntaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Pregunta>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdPregunta", id);
            try
            {
                return await _connection.QueryAsync<Pregunta>(
                    "sp_Pregunta_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Pregunta entity, string user)
        {
            var parameters = new DynamicParameters(entity);
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Pregunta_Mantenimiento",
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
