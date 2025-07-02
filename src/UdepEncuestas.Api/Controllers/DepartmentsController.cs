using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdepEncuestas.Services;

namespace UdepEncuestas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DepartmentsController : ControllerBase
{
    private readonly DepartmentService _service;

    public DepartmentsController(DepartmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var departments = await _service.GetDepartmentsAsync();
        return Ok(departments);
    }
}
