using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface IAsignacion_Componente_ActividadRepository
    {
        Task<IEnumerable<Asignacion_Componente_Actividad>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Asignacion_Componente_Actividad entity, string user);
    }
}
