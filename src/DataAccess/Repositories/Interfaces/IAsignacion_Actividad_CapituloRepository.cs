using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface IAsignacion_Actividad_CapituloRepository
    {
        Task<IEnumerable<Asignacion_Actividad_Capitulo>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Asignacion_Actividad_Capitulo entity, string user);
    }
}
