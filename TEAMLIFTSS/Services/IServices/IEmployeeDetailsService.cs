using TEAMLIFTSS.Models;
using TEAMLIFTSS.Repos.TableModels;

namespace TEAMLIFTSS.Services.IServices;

public interface IEmployeeDetailsService
{
    public Task<List<Employeedetail>> EmpDtlsList();
    public Task<Employeedetail> EmpDetlail(string EmdId);
    public Task<ApiResponse> CreateEmployee(Employeedetail Employee);
    public Task<ApiResponse> UpdateEmployee(Employeedetail Employee);
}
