using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public interface IEncuestaRepository
    {
        Task<IEnumerable<Encuesta>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Encuesta entity, string user);
    }
}
