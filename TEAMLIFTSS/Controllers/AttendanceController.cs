using Microsoft.AspNetCore.Mvc;
using TEAMLIFTSS.Repos.TableModels;
using TEAMLIFTSS.Services.IServices;

namespace TEAMLIFTSS.Controllers
{
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;
        public AttendanceController(IAttendanceService service)
        {
            this._service = service;
        }

        [Route("api/AttendanceDetails")]
        [HttpGet]
        public async Task<IActionResult> GetAttendanceDetails(string EmpId)
        {
            if (EmpId == null)
            {
                return BadRequest(new { result = EmpId + "Input is Empty / Null", data = EmpId });
            }
            else
            {
                var respond = await this._service.GetAttendanceDetails(EmpId);
                if (respond != null)
                {
                    return Ok(respond);
                }
                else
                {
                    return NotFound(respond);
                }
            }
        }

        [Route("api/Attendance")]
        [HttpPost]
        public async Task<IActionResult> PostAttendance(Attendancedetail attendance)
        {
            if (attendance != null)
            {
                var respond = await this._service.PostAttendance(attendance);
                if (respond != null)
                {
                    return Ok(respond);
                }
                else
                {
                    return NotFound(respond);
                }
            }
            else
            {
                return BadRequest(new { result = "Input is Empty / Null", data = attendance });
            }
        }
    }
}
