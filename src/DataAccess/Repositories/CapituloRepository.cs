using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class CapituloRepository : ICapituloRepository
    {
        private readonly IDbConnection _connection;

        public CapituloRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Capitulo>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdCapitulo", id);
            try
            {
                return await _connection.QueryAsync<Capitulo>(
                    "sp_Capitulo_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Capitulo entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdCapitulo", entity.iIdCapitulo);
            parameters.Add("@iIdAsignatura", entity.iIdAsignatura);
            parameters.Add("@cNumeroCapitulo", entity.cNumeroCapitulo);
            parameters.Add("@cNombreCapitulo", entity.cNombreCapitulo);
            parameters.Add("@cDescripcionCapitulo", entity.cDescripcionCapitulo);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Capitulo_Mantenimiento",
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
