using UDEP.Encuestas.Business.Services;
using UDEP.Encuestas.Business.Models;
using UDEP.Encuestas.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UDEP.Encuestas.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PreguntaController : ControllerBase
    {
        private readonly PreguntaService _service;
        private readonly ILogger<PreguntaController> _logger;

        public PreguntaController(PreguntaService service, ILogger<PreguntaController> logger)
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
        public async Task<IActionResult> Post(Pregunta entity)
        {
            var user = User.Identity?.Name ?? "anonymous";
            var result = await _service.RegistrarAsync(entity, user);
            if (result.ResultType == ResultType.Success) return Ok();
            if (result.ResultType == ResultType.SqlError) return BadRequest(result.Message);
            return StatusCode(500, result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Pregunta entity)
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
