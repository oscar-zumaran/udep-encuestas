using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface IOpcion_PreguntaRepository
    {
        Task<IEnumerable<Opcion_Pregunta>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Opcion_Pregunta entity, string user);
    }
}
