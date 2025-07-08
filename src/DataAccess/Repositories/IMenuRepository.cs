using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Menu entity, string user);
        Task<IEnumerable<Menu>> ListarPorRolAsync(int idRol);
    }
}
