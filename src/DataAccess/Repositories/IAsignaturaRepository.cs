using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public interface IAsignaturaRepository
    {
        Task<IEnumerable<Asignatura>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Asignatura entity, string user);
    }
}
