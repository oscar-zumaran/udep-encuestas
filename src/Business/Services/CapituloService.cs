using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class CapituloService : GenericService<Capitulo>
    {
        public CapituloService(IGenericRepository<Capitulo> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Capitulo.iIdCapitulo), user);
    }
}
