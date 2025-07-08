using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Log_AuditoriaService
    {
        private readonly ILog_AuditoriaRepository _repository;

        public Log_AuditoriaService(ILog_AuditoriaRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Log_Auditoria>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Log_Auditoria entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Log_Auditoria entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Log_Auditoria { iIdLogAuditoria = id }, user);
    }
}
