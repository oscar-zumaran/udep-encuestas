using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.Business.Services
{
    public class Oferta_AcademicaService
    {
        private readonly IOferta_AcademicaRepository _repository;

        public Oferta_AcademicaService(IOferta_AcademicaRepository repo)
        {
            _repository = repo;
        }

        public Task<IEnumerable<Oferta_Academica>> ListarAsync(int? id) => _repository.ListarAsync(id);

        public Task RegistrarAsync(Oferta_Academica entity, string user) => _repository.MantenimientoAsync(1, entity, user);

        public Task ActualizarAsync(Oferta_Academica entity, string user) => _repository.MantenimientoAsync(2, entity, user);

        public Task EliminarAsync(int id, string user) => _repository.MantenimientoAsync(3, new Oferta_Academica { iIdOfertaAcademica = id }, user);
    }
}
