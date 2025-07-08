using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public interface ISesion_EncuestaRepository
    {
        Task<IEnumerable<Sesion_Encuesta>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Sesion_Encuesta entity, string user);
    }
}
