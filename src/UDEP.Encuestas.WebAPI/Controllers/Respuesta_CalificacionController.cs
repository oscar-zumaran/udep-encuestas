using UDEP.Encuestas.Business.Services;
using UDEP.Encuestas.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UDEP.Encuestas.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class Respuesta_CalificacionController : ControllerBase
    {
        private readonly Respuesta_CalificacionService _service;
        private readonly ILogger<Respuesta_CalificacionController> _logger;

        public Respuesta_CalificacionController(Respuesta_CalificacionService service, ILogger<Respuesta_CalificacionController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            var result = await _service.ListarAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Respuesta_Calificacion entity)
        {
            var user = User.Identity?.Name ?? "anonymous";
            await _service.RegistrarAsync(entity, user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Respuesta_Calificacion entity)
        {
            var user = User.Identity?.Name ?? "anonymous";
            await _service.ActualizarAsync(entity, user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = User.Identity?.Name ?? "anonymous";
            await _service.EliminarAsync(id, user);
            return Ok();
        }
    }
}
