using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class EncuestaService
    {
        private readonly IEncuestaRepository _repository;

        public EncuestaService(IEncuestaRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Encuesta>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Encuesta entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Encuesta entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Encuesta { iIdEncuesta = id }, user);
    }
}
