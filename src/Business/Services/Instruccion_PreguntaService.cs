using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.Business.Services
{
    public class Instruccion_PreguntaService
    {
        private readonly IInstruccion_PreguntaRepository _repository;

        public Instruccion_PreguntaService(IInstruccion_PreguntaRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Instruccion_Pregunta>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Instruccion_Pregunta entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Instruccion_Pregunta entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Instruccion_Pregunta { iIdInstruccion = id }, user);
    }
}
