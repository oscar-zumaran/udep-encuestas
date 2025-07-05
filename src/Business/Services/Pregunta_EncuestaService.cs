using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Pregunta_EncuestaService : GenericService<Pregunta_Encuesta>
    {
        public Pregunta_EncuestaService(IGenericRepository<Pregunta_Encuesta> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Pregunta_Encuesta.iIdPreguntaEncuesta), user);
    }
}
