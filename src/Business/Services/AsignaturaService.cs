using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class AsignaturaService : GenericService<Asignatura>
    {
        public AsignaturaService(IGenericRepository<Asignatura> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Asignatura.iIdAsignatura), user);
    }
}
