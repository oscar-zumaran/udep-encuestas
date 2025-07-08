using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface ICapituloRepository
    {
        Task<IEnumerable<Capitulo>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Capitulo entity, string user);
    }
}
