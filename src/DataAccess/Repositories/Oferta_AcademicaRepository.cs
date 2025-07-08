using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Oferta_AcademicaRepository : IOferta_AcademicaRepository
    {
        private readonly IDbConnection _connection;

        public Oferta_AcademicaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Oferta_Academica>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdOfertaAcademica", id);
            try
            {
                return await _connection.QueryAsync<Oferta_Academica>(
                    "sp_Oferta_Academica_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Oferta_Academica entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdOfertaAcademica", entity.iIdOfertaAcademica);
            parameters.Add("@iIdPeriodo", entity.iIdPeriodo);
            parameters.Add("@iIdAsignatura", entity.iIdAsignatura);
            parameters.Add("@iNumeroMatriculados", entity.iNumeroMatriculados);
            parameters.Add("@cJefeCurso", entity.cJefeCurso);
            parameters.Add("@iAprobados", entity.iAprobados);
            parameters.Add("@iDesaprobados", entity.iDesaprobados);
            parameters.Add("@iRetirados", entity.iRetirados);
            parameters.Add("@iAnulaciones", entity.iAnulaciones);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Oferta_Academica_Mantenimiento",
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
