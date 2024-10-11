using Microsoft.EntityFrameworkCore;
using TEAMLIFTSS.Models;
using TEAMLIFTSS.Repos;
using TEAMLIFTSS.Repos.TableModels;
using TEAMLIFTSS.Services.IServices;

namespace TEAMLIFTSS.Services;

public class EmployeeDetailsService : IEmployeeDetailsService
{
    private readonly ApplicationDBContext _dbContext;

    public EmployeeDetailsService(ApplicationDBContext dbContext)
    {
        this._dbContext = dbContext;
    }
    public async Task<ApiResponse> CreateEmployee(Employeedetail Employee)
    {
        var respond = new ApiResponse();
        try
        {
            await this._dbContext.Employeedetails.AddAsync(Employee);
            await this._dbContext.SaveChangesAsync();

            respond.Result = $"{Employee.Empid} Employee Created Success.";
            respond.ResponseCode = 200;
        }
        catch (Exception ex)
        {
            respond.Result = $"{Employee.Empid} Employee Creation Failed. \n Exception : {ex.Message}";
            respond.ResponseCode = 404;
        }
        return respond;
    }

    public async Task<Employeedetail> EmpDetlail(string EmdId)
    {
        var Emp = await this._dbContext.Employeedetails.Where(x => x.Empid == EmdId && x.Active == true).FirstOrDefaultAsync();
        if (Emp != null)
        {
            return Emp;
        }
        else
        {
            return null;
        }
    }

    public async Task<List<Employeedetail>> EmpDtlsList()
    {
        return await this._dbContext.Employeedetails.ToListAsync();
    }

    public async Task<ApiResponse> UpdateEmployee(Employeedetail Employee)
    {
        var respond = new ApiResponse();
        try
        {
            var data = await this._dbContext.Employeedetails.Where(x => x.Empid == Employee.Empid && x.Active == true).FirstOrDefaultAsync();
            if (data != null)
            {
                var MakeEmp = new Employeedetail()
                {
                    Empid = data.Empid,
                    Firstname = Employee.Firstname,
                    Middlename = Employee.Middlename,
                    Lastname = Employee.Lastname,
                    Secretcode = data.Secretcode,
                    Department = Employee.Department,
                    Designation = Employee.Designation,
                    Emprole = data.Emprole,
                    Devicedetails = Employee.Devicedetails,
                    Empviolation = Employee.Empviolation,
                    Deviceviolation = Employee.Deviceviolation,
                    Remarks = Employee.Remarks,
                };
            }
            await this._dbContext.SaveChangesAsync();

            respond.Result = $"{Employee.Empid} Employee Created Success.";
            respond.ResponseCode = 200;
        }
        catch (Exception ex)
        {
            respond.Result = $"{Employee.Empid} Employee Creation Failed.  \n Exception : {ex.Message}";
            respond.ResponseCode = 404;
        }
        return respond;
    }
}
