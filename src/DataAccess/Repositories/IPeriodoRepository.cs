using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    /// <summary>
    /// Define las operaciones de acceso a datos para la entidad Periodo.
    /// </summary>
    public interface IPeriodoRepository
    {
        Task<IEnumerable<Periodo>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Periodo periodo, string user);
    }
}
