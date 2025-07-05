using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class ActividadService : GenericService<Actividad>
    {
        public ActividadService(IGenericRepository<Actividad> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Actividad.iIdActividad), user);
    }
}
