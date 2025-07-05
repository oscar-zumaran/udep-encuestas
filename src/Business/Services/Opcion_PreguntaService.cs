using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Opcion_PreguntaService : GenericService<Opcion_Pregunta>
    {
        public Opcion_PreguntaService(IGenericRepository<Opcion_Pregunta> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Opcion_Pregunta.iIdOpcion), user);
    }
}
