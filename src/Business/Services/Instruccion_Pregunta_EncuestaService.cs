using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Instruccion_Pregunta_EncuestaService
    {
        private readonly IInstruccion_Pregunta_EncuestaRepository _repository;

        public Instruccion_Pregunta_EncuestaService(IInstruccion_Pregunta_EncuestaRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Instruccion_Pregunta_Encuesta>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Instruccion_Pregunta_Encuesta entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Instruccion_Pregunta_Encuesta entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Instruccion_Pregunta_Encuesta { iIdInstruccionPreguntaEncuesta = id }, user);
    }
}
