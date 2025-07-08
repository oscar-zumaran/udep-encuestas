using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface IRespuesta_CalificacionRepository
    {
        Task<IEnumerable<Respuesta_Calificacion>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Respuesta_Calificacion entity, string user);
    }
}
