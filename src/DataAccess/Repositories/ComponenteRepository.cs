using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class ComponenteRepository : IComponenteRepository
    {
        private readonly IDbConnection _connection;

        public ComponenteRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Componente>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdComponente", id);
            try
            {
                return await _connection.QueryAsync<Componente>(
                    "sp_Componente_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Componente entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdComponente", entity.iIdComponente);
            parameters.Add("@cSiglas", entity.cSiglas);
            parameters.Add("@cNombreComponente", entity.cNombreComponente);
            parameters.Add("@cDescripcion", entity.cDescripcion);
            parameters.Add("@bEliminarOpcionesEncuesta", entity.bEliminarOpcionesEncuesta);
            parameters.Add("@bVisibleReportes", entity.bVisibleReportes);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Componente_Mantenimiento",
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
