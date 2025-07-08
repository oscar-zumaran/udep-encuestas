using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.Business.Services
{
    public class Respuesta_OpcionService
    {
        private readonly IRespuesta_OpcionRepository _repository;

        public Respuesta_OpcionService(IRespuesta_OpcionRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Respuesta_Opcion>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Respuesta_Opcion entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Respuesta_Opcion entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Respuesta_Opcion { iIdRespuesta = id }, user);
    }
}
