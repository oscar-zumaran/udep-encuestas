using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.Business.Services
{
    /// <summary>
    /// Lógica de negocios para Periodos.
    /// </summary>
    public class PeriodoService
    {
        private readonly IPeriodoRepository _repository;

        public PeriodoService(IPeriodoRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Periodo>> ListarAsync(int? id) => _repository.ListarAsync(id);
        public Task RegistrarAsync(Periodo periodo, string user) => _repository.MantenimientoAsync(1, periodo, user);
        public Task ActualizarAsync(Periodo periodo, string user) => _repository.MantenimientoAsync(2, periodo, user);
        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Periodo { iIdPeriodo = id }, user);
    }
}
