using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    /// <summary>
    /// Define operaciones específicas para la entidad Menu.
    /// </summary>
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        Task<IEnumerable<Menu>> ListarPorRolAsync(int idRol);
    }
}
