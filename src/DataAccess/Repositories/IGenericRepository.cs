namespace UDEP.Encuestas.DataAccess.Repositories
{
    /// <summary>
    /// Interfaz genérica para repositorios de entidades.
    /// </summary>
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> ListarAsync(int? id);
        Task MantenimientoAsync(int operacion, T entity, string user);
    }
}
