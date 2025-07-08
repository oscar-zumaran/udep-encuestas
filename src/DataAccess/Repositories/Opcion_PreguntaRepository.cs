using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Opcion_PreguntaRepository : IOpcion_PreguntaRepository
    {
        private readonly IDbConnection _connection;

        public Opcion_PreguntaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Opcion_Pregunta>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdOpcion", id);
            try
            {
                return await _connection.QueryAsync<Opcion_Pregunta>(
                    "sp_Opcion_Pregunta_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Opcion_Pregunta entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdOpcion", entity.iIdOpcion);
            parameters.Add("@iIdPregunta", entity.iIdPregunta);
            parameters.Add("@cDescripcionOpcion", entity.cDescripcionOpcion);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Opcion_Pregunta_Mantenimiento",
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
