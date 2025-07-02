using Dapper;
using System.Data;
using UdepEncuestas.Core.Models;

namespace UdepEncuestas.Data;

public class DepartmentRepository
{
    private readonly IDbConnection _connection;

    public DepartmentRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _connection.QueryAsync<Department>("SELECT * FROM Departamento");
    }
}
