using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.Business.Services
{
    public class Asignacion_Actividad_CapituloService
    {
        private readonly IAsignacion_Actividad_CapituloRepository _repository;

        public Asignacion_Actividad_CapituloService(IAsignacion_Actividad_CapituloRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Asignacion_Actividad_Capitulo>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Asignacion_Actividad_Capitulo entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Asignacion_Actividad_Capitulo entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Asignacion_Actividad_Capitulo { iIdAsignacion = id }, user);
    }
}
