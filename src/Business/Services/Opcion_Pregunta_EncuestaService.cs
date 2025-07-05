using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Opcion_Pregunta_EncuestaService : GenericService<Opcion_Pregunta_Encuesta>
    {
        public Opcion_Pregunta_EncuestaService(IGenericRepository<Opcion_Pregunta_Encuesta> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Opcion_Pregunta_Encuesta.iIdOpcionPreguntaEncuesta), user);
    }
}
