using UDEP.Encuestas.Business.Services;
using UDEP.Encuestas.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UDEP.Encuestas.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartamentoController : ControllerBase
    {
        private readonly DepartamentoService _service;
        private readonly ILogger<DepartamentoController> _logger;

        public DepartamentoController(DepartamentoService service, ILogger<DepartamentoController> logger)
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
        public async Task<IActionResult> Post(Departamento depto)
        {
            var user = User.Identity?.Name ?? "anonymous";
            await _service.RegistrarAsync(depto, user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Departamento depto)
        {
            var user = User.Identity?.Name ?? "anonymous";
            await _service.ActualizarAsync(depto, user);
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
