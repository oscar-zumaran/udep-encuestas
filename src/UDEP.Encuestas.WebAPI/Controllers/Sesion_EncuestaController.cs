using UDEP.Encuestas.Business.Services;
using UDEP.Encuestas.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Sesion_Encuesta entity)
        {
            var user = User.Identity?.Name ?? "anonymous";
            await _service.RegistrarAsync(entity, user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Sesion_Encuesta entity)
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
