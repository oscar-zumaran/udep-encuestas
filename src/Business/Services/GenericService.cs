using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    /// <summary>
    /// Servicio genérico que encapsula lógica de negocio mínima para entidades.
    /// </summary>
    public class GenericService<T>
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<T>> ListarAsync(int? id) => _repository.ListarAsync(id);
        public Task RegistrarAsync(T entity, string user) => _repository.MantenimientoAsync(1, entity, user);
        public Task ActualizarAsync(T entity, string user) => _repository.MantenimientoAsync(2, entity, user);
        public Task EliminarAsync(int id, string idProperty, string user)
        {
            var obj = Activator.CreateInstance<T>();
            typeof(T).GetProperty(idProperty)?.SetValue(obj, id);
            return _repository.MantenimientoAsync(3, obj!, user);
        }
    }
}
