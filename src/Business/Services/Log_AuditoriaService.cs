using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Log_AuditoriaService : GenericService<Log_Auditoria>
    {
        public Log_AuditoriaService(IGenericRepository<Log_Auditoria> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Log_Auditoria.iIdLogAuditoria), user);
    }
}
