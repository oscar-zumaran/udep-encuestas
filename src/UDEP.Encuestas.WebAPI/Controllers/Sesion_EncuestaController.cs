using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UDEP.Encuestas.Business.Models;
using UDEP.Encuestas.Business.Services;
using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class Sesion_EncuestaController : ControllerBase
    {
        private readonly Sesion_EncuestaService _service;
        private readonly ILogger<Sesion_EncuestaController> _logger;

        public Sesion_EncuestaController(Sesion_EncuestaService service, ILogger<Sesion_EncuestaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            var result = await _service.ListarAsync(id);
            return result.ResultType switch
            {
                ResultType.Success => Ok(result.Data),
                ResultType.SqlError => BadRequest(result.Message),
                _ => StatusCode(500, result.Message)
            };
        }

        [HttpPost]
        public async Task<IActionResult> Post(Sesion_Encuesta entity)
        {
            var user = User.Identity?.Name ?? "anonymous";
            var result = await _service.RegistrarAsync(entity, user);
            if (result.ResultType == ResultType.Success) return Ok();
            if (result.ResultType == ResultType.SqlError) return BadRequest(result.Message);
            return StatusCode(500, result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Sesion_Encuesta entity)
        {
            var user = User.Identity?.Name ?? "anonymous";
            var result = await _service.ActualizarAsync(entity, user);
            if (result.ResultType == ResultType.Success) return Ok();
            if (result.ResultType == ResultType.SqlError) return BadRequest(result.Message);
            return StatusCode(500, result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = User.Identity?.Name ?? "anonymous";
            var result = await _service.EliminarAsync(id, user);
            if (result.ResultType == ResultType.Success) return Ok();
            if (result.ResultType == ResultType.SqlError) return BadRequest(result.Message);
            return StatusCode(500, result.Message);
        }
    }
}
