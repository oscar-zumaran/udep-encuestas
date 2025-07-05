using System.Data;
using Dapper;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    /// <summary>
    /// Repositorio genérico basado en Dapper que ejecuta procedimientos almacenados.
    /// </summary>
    public class GenericRepository<T> : IGenericRepository<T>
    {
        private readonly IDbConnection _connection;
        private readonly string _entityName;
        private readonly string _idParam;

        public GenericRepository(IDbConnection connection, string entityName, string idParam)
        {
            _connection = connection;
            _entityName = entityName;
            _idParam = idParam;
        }

        public async Task<IEnumerable<T>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"@{_idParam}", id);
            return await _connection.QueryAsync<T>($"sp_{_entityName}_Listar", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task MantenimientoAsync(int operacion, T entity, string user)
        {
            var parameters = new DynamicParameters(entity);
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);

            await _connection.ExecuteAsync(
                $"sp_{_entityName}_Mantenimiento",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}
