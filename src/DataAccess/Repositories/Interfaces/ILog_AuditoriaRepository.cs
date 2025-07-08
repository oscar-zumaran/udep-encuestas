using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface ILog_AuditoriaRepository
    {
        Task<IEnumerable<Log_Auditoria>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Log_Auditoria entity, string user);
    }
}
