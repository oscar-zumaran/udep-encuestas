using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly IDbConnection _connection;

        public DepartamentoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Departamento>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdDepartamento", id);
            try
            {
                return await _connection.QueryAsync<Departamento>(
                    "sp_Departamento_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Departamento depto, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdDepartamento", depto.iIdDepartamento);
            parameters.Add("@cNombreDepartamento", depto.cNombreDepartamento);
            parameters.Add("@cCorreoInstitucional", depto.cCorreoInstitucional);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Departamento_Mantenimiento",
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
