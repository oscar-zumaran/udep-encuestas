using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface IPregunta_EncuestaRepository
    {
        Task<IEnumerable<Pregunta_Encuesta>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Pregunta_Encuesta entity, string user);
    }
}
