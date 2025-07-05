using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Respuesta_LikertService : GenericService<Respuesta_Likert>
    {
        public Respuesta_LikertService(IGenericRepository<Respuesta_Likert> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Respuesta_Likert.iIdRespuesta), user);
    }
}
