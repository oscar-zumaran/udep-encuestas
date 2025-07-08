using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class AsignaturaRepository : IAsignaturaRepository
    {
        private readonly IDbConnection _connection;

        public AsignaturaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Asignatura>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdAsignatura", id);
            try
            {
                return await _connection.QueryAsync<Asignatura>(
                    "sp_Asignatura_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Asignatura entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdAsignatura", entity.iIdAsignatura);
            parameters.Add("@iIdDepartamento", entity.iIdDepartamento);
            parameters.Add("@iIdComponente", entity.iIdComponente);
            parameters.Add("@cSiglas", entity.cSiglas);
            parameters.Add("@cNombreAsignatura", entity.cNombreAsignatura);
            parameters.Add("@iCiclo", entity.iCiclo);
            parameters.Add("@iAnio", entity.iAnio);
            parameters.Add("@iCreditos", entity.iCreditos);
            parameters.Add("@bTieneCapitulos", entity.bTieneCapitulos);
            parameters.Add("@iNumeroCapitulos", entity.iNumeroCapitulos);
            parameters.Add("@bTieneSedeHospitalaria", entity.bTieneSedeHospitalaria);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Asignatura_Mantenimiento",
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
