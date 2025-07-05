using System.Data;
using Dapper;
using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    /// <summary>
    /// Implementación de IMenuRepository utilizando procedimientos almacenados.
    /// </summary>
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        private readonly IDbConnection _connection;

        public MenuRepository(IDbConnection connection)
            : base(connection, "Menu", "iIdMenu")
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Menu>> ListarPorRolAsync(int idRol)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdRol", idRol);
            return await _connection.QueryAsync<Menu>(
                "sp_MenuPorRol_Listar",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}
