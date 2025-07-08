using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories.Interfaces
{
    public interface IOferta_AcademicaRepository
    {
        Task<IEnumerable<Oferta_Academica>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, Oferta_Academica entity, string user);
    }
}
