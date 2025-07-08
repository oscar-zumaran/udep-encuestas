using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Respuesta_LikertService
    {
        private readonly IRespuesta_LikertRepository _repository;

        public Respuesta_LikertService(IRespuesta_LikertRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Respuesta_Likert>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Respuesta_Likert entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Respuesta_Likert entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Respuesta_Likert { iIdRespuesta = id }, user);
    }
}
