using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.Business.Services
{
    public class DimensionService
    {
        private readonly IDimensionRepository _repository;

        public DimensionService(IDimensionRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Dimension>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Dimension entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Dimension entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Dimension { iIdDimension = id }, user);
    }
}
