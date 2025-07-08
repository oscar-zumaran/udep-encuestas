using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Respuesta_TextoService
    {
        private readonly IRespuesta_TextoRepository _repository;

        public Respuesta_TextoService(IRespuesta_TextoRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Respuesta_Texto>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Respuesta_Texto entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Respuesta_Texto entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Respuesta_Texto { iIdRespuesta = id }, user);
    }
}
