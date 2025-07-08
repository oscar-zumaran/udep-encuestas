using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public interface IInstruccion_PreguntaRepository
    {
        Task<IEnumerable<Instruccion_Pregunta>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Instruccion_Pregunta entity, string user);
    }
}
