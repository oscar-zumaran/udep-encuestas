using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface IDimensionRepository
    {
        Task<IEnumerable<Dimension>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Dimension entity, string user);
    }
}
