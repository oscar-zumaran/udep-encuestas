using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public interface IRespuesta_LikertRepository
    {
        Task<IEnumerable<Respuesta_Likert>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Respuesta_Likert entity, string user);
    }
}
