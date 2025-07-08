using Microsoft.Data.SqlClient;
using UDEP.Encuestas.Business.Models;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;
using UDEP.Encuestas.DataAccess.Repositories.Interfaces;

namespace UDEP.Encuestas.Business.Services
{
    /// <summary>
    /// Lógica de negocios para Actividad.
    /// </summary>
    public class ActividadService
    {
        private readonly IActividadRepository _repository;

        public ActividadService(IActividadRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<IEnumerable<Actividad>>> ListarAsync(int? id)
        {
            try
            {
                var data = await _repository.ListarAsync(id);
                return new ServiceResult<IEnumerable<Actividad>> { ResultType = ResultType.Success, Data = data };
            }
            catch (DataAccessException ex)
            {
                return new ServiceResult<IEnumerable<Actividad>> { ResultType = ResultType.SqlError, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new ServiceResult<IEnumerable<Actividad>> { ResultType = ResultType.UnexpectedError, Message = ex.Message };
            }
        }

        public async Task<ServiceResult<bool>> RegistrarAsync(Actividad entity, string user)
        {
            try
            {
                await _repository.MantenimientoAsync(1, entity, user);
                return new ServiceResult<bool> { ResultType = ResultType.Success, Data = true };
            }
            catch (DataAccessException ex)
            {
                return new ServiceResult<bool> { ResultType = ResultType.SqlError, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool> { ResultType = ResultType.UnexpectedError, Message = ex.Message };
            }
        }

        public async Task<ServiceResult<bool>> ActualizarAsync(Actividad entity, string user)
        {
            try
            {
                await _repository.MantenimientoAsync(2, entity, user);
                return new ServiceResult<bool> { ResultType = ResultType.Success, Data = true };
            }
            catch (DataAccessException ex)
            {
                return new ServiceResult<bool> { ResultType = ResultType.SqlError, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool> { ResultType = ResultType.UnexpectedError, Message = ex.Message };
            }
        }

        public async Task<ServiceResult<bool>> EliminarAsync(int id, string user)
        {
            try
            {
                await _repository.MantenimientoAsync(3, new Actividad { iIdActividad = id }, user);
                return new ServiceResult<bool> { ResultType = ResultType.Success, Data = true };
            }
            catch (DataAccessException ex)
            {
                return new ServiceResult<bool> { ResultType = ResultType.SqlError, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool> { ResultType = ResultType.UnexpectedError, Message = ex.Message };
            }
        }
    }
}
