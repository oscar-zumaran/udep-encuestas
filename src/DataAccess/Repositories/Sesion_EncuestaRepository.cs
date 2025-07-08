using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

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
            var parameters = new DynamicParameters(entity);
            parameters.Add("@OPERACION", operacion);
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
