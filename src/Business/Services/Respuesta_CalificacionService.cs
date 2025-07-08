using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.Business.Services
{
    public class Respuesta_CalificacionService
    {
        private readonly IRespuesta_CalificacionRepository _repository;

        public Respuesta_CalificacionService(IRespuesta_CalificacionRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Respuesta_Calificacion>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Respuesta_Calificacion entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Respuesta_Calificacion entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Respuesta_Calificacion { iIdRespuesta = id }, user);
    }
}
