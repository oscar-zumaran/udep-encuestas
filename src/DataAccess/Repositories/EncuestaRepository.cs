using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class EncuestaRepository : IEncuestaRepository
    {
        private readonly IDbConnection _connection;

        public EncuestaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Encuesta>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdEncuesta", id);
            try
            {
                return await _connection.QueryAsync<Encuesta>(
                    "sp_Encuesta_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Encuesta entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdEncuesta", entity.iIdEncuesta);
            parameters.Add("@iIdOfertaAcademica", entity.iIdOfertaAcademica);
            parameters.Add("@cTitulo", entity.cTitulo);
            parameters.Add("@cInstrucciones", entity.cInstrucciones);
            parameters.Add("@fFechaHoraInicio", entity.fFechaHoraInicio);
            parameters.Add("@fFechaHoraFin", entity.fFechaHoraFin);
            parameters.Add("@bEsAnonima", entity.bEsAnonima);
            parameters.Add("@bActiva", entity.bActiva);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Encuesta_Mantenimiento",
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
