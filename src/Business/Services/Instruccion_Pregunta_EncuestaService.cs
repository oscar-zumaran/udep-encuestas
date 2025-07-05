using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Instruccion_Pregunta_EncuestaService : GenericService<Instruccion_Pregunta_Encuesta>
    {
        public Instruccion_Pregunta_EncuestaService(IGenericRepository<Instruccion_Pregunta_Encuesta> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Instruccion_Pregunta_Encuesta.iIdInstruccionPreguntaEncuesta), user);
    }
}
