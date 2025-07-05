using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    /// <summary>
    /// Lógica de negocios para Componente.
    /// </summary>
    public class ComponenteService : GenericService<Componente>
    {
        public ComponenteService(IGenericRepository<Componente> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Componente.iIdComponente), user);
    }
}
