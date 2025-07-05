using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Oferta_AcademicaService : GenericService<Oferta_Academica>
    {
        public Oferta_AcademicaService(IGenericRepository<Oferta_Academica> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Oferta_Academica.iIdOfertaAcademica), user);
    }
}
