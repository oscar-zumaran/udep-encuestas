using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class MenuService
    {
        private readonly IMenuRepository _repository;

        public MenuService(IMenuRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Menu>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task<IEnumerable<Menu>> ListarPorRolAsync(int idRol) => _repository.ListarPorRolAsync(idRol);

        public Task RegistrarAsync(Menu entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Menu entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Menu { iIdMenu = id }, user);
    }
}
