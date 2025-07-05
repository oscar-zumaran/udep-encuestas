using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class DimensionService : GenericService<Dimension>
    {
        public DimensionService(IGenericRepository<Dimension> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Dimension.iIdDimension), user);
    }
}
