using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UDEP.Encuestas.Business.Services;
using UDEP.Encuestas.DataAccess.Entities;

namespace UDEP.Encuestas.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PeriodoController : ControllerBase
    {
        private readonly PeriodoService _service;
        private readonly ILogger<PeriodoController> _logger;

        public PeriodoController(PeriodoService service, ILogger<PeriodoController> logger)
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
        public async Task<IActionResult> Post(Periodo periodo)
        {
            var user = User.Identity?.Name ?? "anonymous";
            await _service.RegistrarAsync(periodo, user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Periodo periodo)
        {
            var user = User.Identity?.Name ?? "anonymous";
            await _service.ActualizarAsync(periodo, user);
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
