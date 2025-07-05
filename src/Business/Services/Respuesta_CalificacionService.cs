using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Respuesta_CalificacionService : GenericService<Respuesta_Calificacion>
    {
        public Respuesta_CalificacionService(IGenericRepository<Respuesta_Calificacion> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Respuesta_Calificacion.iIdRespuesta), user);
    }
}
