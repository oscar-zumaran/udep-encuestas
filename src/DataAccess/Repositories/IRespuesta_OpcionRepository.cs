using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public interface IRespuesta_OpcionRepository
    {
        Task<IEnumerable<Respuesta_Opcion>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Respuesta_Opcion entity, string user);
    }
}
