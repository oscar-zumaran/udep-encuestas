using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface IComponenteRepository
    {
        Task<IEnumerable<Componente>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Componente entity, string user);
    }
}
