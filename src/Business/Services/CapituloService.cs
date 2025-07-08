using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class CapituloService
    {
        private readonly ICapituloRepository _repository;

        public CapituloService(ICapituloRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Capitulo>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Capitulo entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Capitulo entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Capitulo { iIdCapitulo = id }, user);
    }
}
