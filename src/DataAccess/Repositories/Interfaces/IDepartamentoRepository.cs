using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// Define las operaciones de acceso a datos para la entidad Departamento.
    /// </summary>
    public interface IDepartamentoRepository
    {
        Task<IEnumerable<Departamento>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Departamento depto, string user);
    }
}
