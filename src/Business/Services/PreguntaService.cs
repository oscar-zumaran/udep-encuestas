using UDEP.Encuestas.Business.Models;
using UDEP.Encuestas.DataAccess.Entities;
using UDEP.Encuestas.DataAccess.Exceptions;
using UDEP.Encuestas.DataAccess.Repositories;

namespace UDEP.Encuestas.Business.Services
{
    public class PreguntaService
    {
        private readonly IPreguntaRepository _repository;

        public PreguntaService(IPreguntaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<IEnumerable<Pregunta>>> ListarAsync(int? id)
        {
            try
            {
                var data = await _repository.ListarAsync(id);
                return new ServiceResult<IEnumerable<Pregunta>> { ResultType = ResultType.Success, Data = data };
            }
            catch (DataAccessException ex)
            {
                return new ServiceResult<IEnumerable<Pregunta>> { ResultType = ResultType.SqlError, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new ServiceResult<IEnumerable<Pregunta>> { ResultType = ResultType.UnexpectedError, Message = ex.Message };
            }
        }

        public async Task<ServiceResult<bool>> RegistrarAsync(Pregunta entity, string user)
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

        public async Task<ServiceResult<bool>> ActualizarAsync(Pregunta entity, string user)
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
                await _repository.MantenimientoAsync(3, new Pregunta { iIdPregunta = id }, user);
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
