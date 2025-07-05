using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class EncuestaService : GenericService<Encuesta>
    {
        public EncuestaService(IGenericRepository<Encuesta> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Encuesta.iIdEncuesta), user);
    }
}
