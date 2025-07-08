using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class ActividadRepository : IActividadRepository
    {
        private readonly IDbConnection _connection;

        public ActividadRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Actividad>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdActividad", id);
            try
            {
                return await _connection.QueryAsync<Actividad>(
                    "sp_Actividad_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Actividad entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdActividad", entity.iIdActividad);
            parameters.Add("@cNombreActividad", entity.cNombreActividad);
            parameters.Add("@cDescripcionActividad", entity.cDescripcionActividad);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Actividad_Mantenimiento",
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
