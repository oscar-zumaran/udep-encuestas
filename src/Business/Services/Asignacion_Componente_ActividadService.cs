using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Asignacion_Componente_ActividadService
    {
        private readonly IAsignacion_Componente_ActividadRepository _repository;

        public Asignacion_Componente_ActividadService(IAsignacion_Componente_ActividadRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Asignacion_Componente_Actividad>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Asignacion_Componente_Actividad entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Asignacion_Componente_Actividad entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Asignacion_Componente_Actividad { iIdAsignacion = id }, user);
    }
}
