using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Asignacion_Actividad_CapituloService : GenericService<Asignacion_Actividad_Capitulo>
    {
        public Asignacion_Actividad_CapituloService(IGenericRepository<Asignacion_Actividad_Capitulo> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Asignacion_Actividad_Capitulo.iIdAsignacion), user);
    }
}
