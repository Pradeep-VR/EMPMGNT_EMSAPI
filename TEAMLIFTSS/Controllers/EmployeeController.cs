using Microsoft.AspNetCore.Mvc;
using TEAMLIFTSS.Models;
using TEAMLIFTSS.Repos.TableModels;
using TEAMLIFTSS.Services.IServices;

namespace TEAMLIFTSS.Controllers;

[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeDetailsService _service;
    public EmployeeController(IEmployeeDetailsService service)
    {
        this._service = service;
    }

    [Route("api/Employees")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var respond = await this._service.EmpDtlsList();
        if (respond != null)
        {
            return Ok(new { result = "Success", data = respond });
        }
        else
        {
            return NotFound(new { result = "Fail", data = respond });
        }
    }

    [Route("api/Employee")]
    [HttpGet]
    public async Task<IActionResult> Get(string EmpId)
    {
        if (string.IsNullOrEmpty(EmpId))
        {
            return BadRequest(new { result = EmpId + "Input is Empty / Null", data = EmpId });
        }
        else
        {
            var respond = await this._service.EmpDetlail(EmpId);
            if (respond != null)
            {
                return Ok(new { result = "Success", data = respond });
            }
            else
            {
                return NotFound(new { result = "Fail", data = respond });
            }
        }
    }

    [Route("api/MakeEmployee")]
    [HttpPost]
    public async Task<IActionResult> CreateEmployee(EmpDtlsCreate Employee)
    {
        if (Employee == null)
        {
            return BadRequest(new { result = "Input is Empty / Null", data = Employee });
        }
        else
        {
            var employee = new Employeedetail()
            {
                Empid = Employee.Empid,
                Firstname = Employee.Firstname,
                Middlename = "",
                Lastname = "",
                Secretcode = Employee.Secretcode,
                Active = true,
                Department = Employee.Department,
                Designation = Employee.Designation,
                Emprole = Employee.Emprole,
                Registeredby = Employee.Registeredby,
                Registereddate = Employee.Registereddate,
                Devicedetails = "",
                Empviolation = false,
                Deviceviolation = 0,
                Remarks = "",
            };

            var respond = await this._service.CreateEmployee(employee);
            if (respond != null)
            {
                return Ok(new { result = "Success", data = respond });
            }
            else
            {
                return NotFound(new { result = "Fail", data = respond });
            }
        }
    }

    [Route("api/ForgetPassword")]
    [HttpPost]
    public async Task<IActionResult> ForgetPassword(Employeedetail emp)
    {
        if (emp != null)
        {
            var respond = await this._service.UpdateEmployee(emp);
            if (respond != null)
            {
                return Ok(new { result = "Success", data = respond });
            }
            else
            {
                return NotFound(new { result = "Fail", data = respond });
            }
        }
        else
        {
            return BadRequest(new { result = "Input is Empty / Null", data = emp });
        }
    }
}
