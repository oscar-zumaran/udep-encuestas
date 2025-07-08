using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Asignacion_Actividad_CapituloRepository : IAsignacion_Actividad_CapituloRepository
    {
        private readonly IDbConnection _connection;

        public Asignacion_Actividad_CapituloRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Asignacion_Actividad_Capitulo>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdAsignacion", id);
            try
            {
                return await _connection.QueryAsync<Asignacion_Actividad_Capitulo>(
                    "sp_Asignacion_Actividad_Capitulo_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Asignacion_Actividad_Capitulo entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdAsignacion", entity.iIdAsignacion);
            parameters.Add("@iIdAsignatura", entity.iIdAsignatura);
            parameters.Add("@iIdCapitulo", entity.iIdCapitulo);
            parameters.Add("@iIdActividad", entity.iIdActividad);
            parameters.Add("@cUsuarioAsignador", entity.cUsuarioAsignador);
            parameters.Add("@fFechaAsignacion", entity.fFechaAsignacion);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Asignacion_Actividad_Capitulo_Mantenimiento",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }
    }
}
