using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Log_AccesoService : GenericService<Log_Acceso>
    {
        public Log_AccesoService(IGenericRepository<Log_Acceso> repo) : base(repo) { }
        public Task EliminarAsync(int id, string user) => base.EliminarAsync(id, nameof(Log_Acceso.iIdLogAcceso), user);
    }
}
