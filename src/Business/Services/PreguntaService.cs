using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class PreguntaService : GenericService<Pregunta>
    {
        public PreguntaService(IGenericRepository<Pregunta> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Pregunta.iIdPregunta), user);
    }
}
