using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface ILog_AccesoRepository
    {
        Task<IEnumerable<Log_Acceso>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Log_Acceso entity, string user);
    }
}
