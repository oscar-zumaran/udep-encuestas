using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface IOpcion_Pregunta_EncuestaRepository
    {
        Task<IEnumerable<Opcion_Pregunta_Encuesta>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Opcion_Pregunta_Encuesta entity, string user);
    }
}
