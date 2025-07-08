using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Respuesta_TextoRepository : IRespuesta_TextoRepository
    {
        private readonly IDbConnection _connection;

        public Respuesta_TextoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Respuesta_Texto>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdRespuesta", id);
            try
            {
                return await _connection.QueryAsync<Respuesta_Texto>(
                    "sp_Respuesta_Texto_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Respuesta_Texto entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdRespuesta", entity.iIdRespuesta);
            parameters.Add("@iIdSesion", entity.iIdSesion);
            parameters.Add("@iIdPreguntaEncuesta", entity.iIdPreguntaEncuesta);
            parameters.Add("@cTextoRespuesta", entity.cTextoRespuesta);
            parameters.Add("@cUsuarioRespuesta", entity.cUsuarioRespuesta);
            parameters.Add("@fFechaRespuesta", entity.fFechaRespuesta);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Respuesta_Texto_Mantenimiento",
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
