using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.Business.Services
{
    public class ComponenteService
    {
        private readonly IComponenteRepository _repository;

        public ComponenteService(IComponenteRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Componente>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Componente entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Componente entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Componente { iIdComponente = id }, user);
    }
}
