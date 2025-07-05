using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Respuesta_TextoService : GenericService<Respuesta_Texto>
    {
        public Respuesta_TextoService(IGenericRepository<Respuesta_Texto> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Respuesta_Texto.iIdRespuesta), user);
    }
}
