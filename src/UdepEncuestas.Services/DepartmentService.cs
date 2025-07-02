using UdepEncuestas.Core.Models;
using UdepEncuestas.Data;

namespace UdepEncuestas.Services;

public class DepartmentService
{
    private readonly DepartmentRepository _repository;

    public DepartmentService(DepartmentRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Department>> GetDepartmentsAsync()
    {
        return _repository.GetAllAsync();
    }
}
