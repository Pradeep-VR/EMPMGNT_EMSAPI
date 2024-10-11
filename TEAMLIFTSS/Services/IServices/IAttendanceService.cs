using TEAMLIFTSS.Models;
using TEAMLIFTSS.Repos.TableModels;

namespace TEAMLIFTSS.Services.IServices
{
    public interface IAttendanceService
    {
        public Task<Api> PostAttendance(Attendancedetail Attence);
        public Task<Api> GetAttendanceDetails(string strEmpId);
    }
}
