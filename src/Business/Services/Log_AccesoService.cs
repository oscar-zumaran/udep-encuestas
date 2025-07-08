using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Log_AccesoService
    {
        private readonly ILog_AccesoRepository _repository;

        public Log_AccesoService(ILog_AccesoRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Log_Acceso>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Log_Acceso entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Log_Acceso entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Log_Acceso { iIdLogAcceso = id }, user);
    }
}
