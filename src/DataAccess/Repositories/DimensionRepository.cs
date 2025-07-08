using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class DimensionRepository : IDimensionRepository
    {
        private readonly IDbConnection _connection;

        public DimensionRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Dimension>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdDimension", id);
            try
            {
                return await _connection.QueryAsync<Dimension>(
                    "sp_Dimension_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Dimension entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdDimension", entity.iIdDimension);
            parameters.Add("@cNombreDimension", entity.cNombreDimension);
            parameters.Add("@cDescripcion", entity.cDescripcion);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Dimension_Mantenimiento",
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
