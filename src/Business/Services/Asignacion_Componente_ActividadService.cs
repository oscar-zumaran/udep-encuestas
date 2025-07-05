using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Asignacion_Componente_ActividadService : GenericService<Asignacion_Componente_Actividad>
    {
        public Asignacion_Componente_ActividadService(IGenericRepository<Asignacion_Componente_Actividad> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Asignacion_Componente_Actividad.iIdAsignacion), user);
    }
}
