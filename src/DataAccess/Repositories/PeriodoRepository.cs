using System.Data;
using Dapper;
using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    /// <summary>
    /// Implementación Dapper para Periodo utilizando procedimientos almacenados.
    /// </summary>
    public class PeriodoRepository : IPeriodoRepository
    {
        private readonly IDbConnection _connection;

        public PeriodoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Periodo>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdPeriodo", id);
            return await _connection.QueryAsync<Periodo>(
                "sp_Periodo_Listar",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task MantenimientoAsync(int operacion, Periodo periodo, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdPeriodo", periodo.iIdPeriodo);
            parameters.Add("@iAnioAcademico", periodo.iAnioAcademico);
            parameters.Add("@cNumeroPeriodo", periodo.cNumeroPeriodo);
            parameters.Add("@cNombrePeriodo", periodo.cNombrePeriodo);
            parameters.Add("@fFechaInicio", periodo.fFechaInicio);
            parameters.Add("@fFechaCulminacion", periodo.fFechaCulminacion);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);

            await _connection.ExecuteAsync(
                "sp_Periodo_Mantenimiento",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}
