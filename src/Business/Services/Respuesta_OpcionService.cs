using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Respuesta_OpcionService : GenericService<Respuesta_Opcion>
    {
        public Respuesta_OpcionService(IGenericRepository<Respuesta_Opcion> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Respuesta_Opcion.iIdRespuesta), user);
    }
}
