using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;

namespace UDEP.Encuestas.DataAccess.Repositories
{
    public class Respuesta_OpcionRepository : IRespuesta_OpcionRepository
    {
        private readonly IDbConnection _connection;

        public Respuesta_OpcionRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Respuesta_Opcion>> ListarAsync(int? id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iIdRespuesta", id);
            try
            {
                return await _connection.QueryAsync<Respuesta_Opcion>(
                    "sp_Respuesta_Opcion_Listar",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        public async Task MantenimientoAsync(int operacion, Respuesta_Opcion entity, string user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OPERACION", operacion);
            parameters.Add("@iIdRespuesta", entity.iIdRespuesta);
            parameters.Add("@iIdSesion", entity.iIdSesion);
            parameters.Add("@iIdPreguntaEncuesta", entity.iIdPreguntaEncuesta);
            parameters.Add("@iIdOpcionSeleccionada", entity.iIdOpcionSeleccionada);
            parameters.Add("@cUsuarioRespuesta", entity.cUsuarioRespuesta);
            parameters.Add("@fFechaRespuesta", entity.fFechaRespuesta);
            parameters.Add("@cRegUser", user);
            parameters.Add("@cUpdUser", user);
            try
            {
                await _connection.ExecuteAsync(
                    "sp_Respuesta_Opcion_Mantenimiento",
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
