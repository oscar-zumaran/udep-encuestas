using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Opcion_Pregunta_EncuestaService
    {
        private readonly IOpcion_Pregunta_EncuestaRepository _repository;

        public Opcion_Pregunta_EncuestaService(IOpcion_Pregunta_EncuestaRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Opcion_Pregunta_Encuesta>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Opcion_Pregunta_Encuesta entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Opcion_Pregunta_Encuesta entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Opcion_Pregunta_Encuesta { iIdOpcionPreguntaEncuesta = id }, user);
    }
}
