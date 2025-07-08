using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    /// <summary>
    /// Implementación de IMenuRepository utilizando procedimientos almacenados.
    /// </summary>
    public class MenuRepository : IMenuRepository
    {
        private readonly IDbConnection _connection;

        public MenuRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Menu>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdMenu", id);
            try
            {
                return await _connection.QueryAsync<Menu>(
                    "sp_Menu_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Menu entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdMenu", entity.iIdMenu);
            parameters.Add("@cNombre", entity.cNombre);
            parameters.Add("@cUrl", entity.cUrl);
            parameters.Add("@iNivel", entity.iNivel);
            parameters.Add("@iIdPadre", entity.iIdPadre);
            parameters.Add("@iOrden", entity.iOrden);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Menu_Mantenimiento",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
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
