using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public interface IInstruccion_Pregunta_EncuestaRepository
    {
        Task<IEnumerable<Instruccion_Pregunta_Encuesta>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Instruccion_Pregunta_Encuesta entity, string user);
    }
}
