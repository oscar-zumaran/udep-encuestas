using System.Collections.Generic;
using System.Threading.Tasks;
using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public interface IActividadRepository
    {
        Task<IEnumerable<Actividad>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Actividad entity, string user);
    }
}
