using UDEP.Encuestas.Business.Models;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class Sesion_EncuestaService
    {
        private readonly ISesion_EncuestaRepository _repository;

        public Sesion_EncuestaService(ISesion_EncuestaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<IEnumerable<Sesion_Encuesta>>> ListarAsync(int? id)
        {
            try
            {
                var data = await _repository.ListarAsync(id);
                return new ServiceResult<IEnumerable<Sesion_Encuesta>> { ResultType = ResultType.Success, Data = data };
            }
            catch (DataAccessException ex)
            {
                return new ServiceResult<IEnumerable<Sesion_Encuesta>> { ResultType = ResultType.SqlError, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new ServiceResult<IEnumerable<Sesion_Encuesta>> { ResultType = ResultType.UnexpectedError, Message = ex.Message };
            }
        }

        public async Task<ServiceResult<bool>> RegistrarAsync(Sesion_Encuesta entity, string user)
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

        public async Task<ServiceResult<bool>> ActualizarAsync(Sesion_Encuesta entity, string user)
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
                await _repository.MantenimientoAsync(3, new Sesion_Encuesta { iIdSesion = id }, user);
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
