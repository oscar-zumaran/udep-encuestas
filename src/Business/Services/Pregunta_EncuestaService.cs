using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.Business.Services
{
    public class Pregunta_EncuestaService
    {
        private readonly IPregunta_EncuestaRepository _repository;

        public Pregunta_EncuestaService(IPregunta_EncuestaRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Pregunta_Encuesta>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Pregunta_Encuesta entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Pregunta_Encuesta entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Pregunta_Encuesta { iIdPreguntaEncuesta = id }, user);
    }
}
