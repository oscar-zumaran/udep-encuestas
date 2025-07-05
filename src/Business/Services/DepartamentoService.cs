using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    /// <summary>
    /// Lógica de negocios para Departamentos.
    /// </summary>
    public class DepartamentoService
    {
        private readonly IDepartamentoRepository _repository;

        public DepartamentoService(IDepartamentoRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Departamento>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Departamento depto, string user) => _repository.MantenimientoAsync(1, depto, user);

        public Task ActualizarAsync(Departamento depto, string user) => _repository.MantenimientoAsync(2, depto, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Departamento { iIdDepartamento = id }, user);
    }
}
