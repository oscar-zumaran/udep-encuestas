using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Sesion_EncuestaService : GenericService<Sesion_Encuesta>
    {
        public Sesion_EncuestaService(IGenericRepository<Sesion_Encuesta> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Sesion_Encuesta.iIdSesion), user);
    }
}
