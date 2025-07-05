using UDEP.Encuestas.Business.Services;
using UDEP.Encuestas.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UDEP.Encuestas.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class Asignacion_Actividad_CapituloController : ControllerBase
    {
        private readonly Asignacion_Actividad_CapituloService _service;
        private readonly ILogger<Asignacion_Actividad_CapituloController> _logger;

        public Asignacion_Actividad_CapituloController(Asignacion_Actividad_CapituloService service, ILogger<Asignacion_Actividad_CapituloController> logger)
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
        public async Task<IActionResult> Post(Asignacion_Actividad_Capitulo entity)
        {
            var user = User.Identity?.Name ?? "anonymous";
            await _service.RegistrarAsync(entity, user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Asignacion_Actividad_Capitulo entity)
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
