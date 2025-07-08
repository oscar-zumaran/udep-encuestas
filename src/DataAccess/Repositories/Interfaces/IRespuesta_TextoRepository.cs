using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface IRespuesta_TextoRepository
    {
        Task<IEnumerable<Respuesta_Texto>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Respuesta_Texto entity, string user);
    }
}
