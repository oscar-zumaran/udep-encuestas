using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    /// <summary>
    /// Lógica de negocios para la entidad Menu.
    /// </summary>
    public class MenuService : GenericService<Menu>
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository repo) : base(repo)
        {
            _menuRepository = repo;
        }

        public Task<IEnumerable<Menu>> ListarPorRolAsync(int idRol) => _menuRepository.ListarPorRolAsync(idRol);

        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Menu.iIdMenu), user);
    }
}
