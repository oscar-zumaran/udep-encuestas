using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Instruccion_PreguntaService : GenericService<Instruccion_Pregunta>
    {
        public Instruccion_PreguntaService(IGenericRepository<Instruccion_Pregunta> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Instruccion_Pregunta.iIdInstruccion), user);
    }
}
