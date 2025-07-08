using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Asignacion_Componente_ActividadRepository : IAsignacion_Componente_ActividadRepository
    {
        private readonly IDbConnection _connection;

        public Asignacion_Componente_ActividadRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Asignacion_Componente_Actividad>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdAsignacion", id);
            try
            {
                return await _connection.QueryAsync<Asignacion_Componente_Actividad>(
                    "sp_Asignacion_Componente_Actividad_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Asignacion_Componente_Actividad entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdAsignacion", entity.iIdAsignacion);
            parameters.Add("@iIdActividad", entity.iIdActividad);
            parameters.Add("@iIdComponente", entity.iIdComponente);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Asignacion_Componente_Actividad_Mantenimiento",
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
