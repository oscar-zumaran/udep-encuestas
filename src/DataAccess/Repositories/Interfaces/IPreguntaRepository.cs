using System.Collections.Generic;
using System.Threading.Tasks;
using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface IPreguntaRepository
    {
        Task<IEnumerable<Pregunta>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Pregunta entity, string user);
    }
}
