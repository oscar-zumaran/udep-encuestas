using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Opcion_PreguntaService
    {
        private readonly IOpcion_PreguntaRepository _repository;

        public Opcion_PreguntaService(IOpcion_PreguntaRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Opcion_Pregunta>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Opcion_Pregunta entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Opcion_Pregunta entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Opcion_Pregunta { iIdOpcion = id }, user);
    }
}
