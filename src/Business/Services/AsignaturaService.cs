using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.Business.Services
{
    public class AsignaturaService
    {
        private readonly IAsignaturaRepository _repository;

        public AsignaturaService(IAsignaturaRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Asignatura>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Asignatura entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Asignatura entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Asignatura { iIdAsignatura = id }, user);
    }
}
