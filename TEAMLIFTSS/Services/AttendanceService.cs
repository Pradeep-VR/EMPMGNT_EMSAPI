using Microsoft.EntityFrameworkCore;
using TEAMLIFTSS.Models;
using TEAMLIFTSS.Repos;
using TEAMLIFTSS.Repos.TableModels;
using TEAMLIFTSS.Services.IServices;

namespace TEAMLIFTSS.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDBContext _dbContext;
        public AttendanceService(ApplicationDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Api> GetAttendanceDetails(string strEmpId)
        {
            var empDtls = await this._dbContext.Employeedetails.Where(x => x.Empid == strEmpId && x.Empviolation == false && x.Deviceviolation < 3).FirstOrDefaultAsync();
            if (empDtls != null)
            {
                var data = await this._dbContext.Attendancedetails.Where(x => x.Employeeid == empDtls.Empid).ToListAsync();
                if (data != null)
                {
                    return new Api() { result = "Success", data = data };
                }
                else
                {
                    return new Api() { result = "Fail", data = null };
                }
            }
            else
            {
                return new Api() { result = "Fail", data = null };
            }
        }

        public async Task<Api> PostAttendance(Attendancedetail Attence)
        {
            var Qry = string.Empty;
            var empDtl = await this._dbContext.Employeedetails.Where(x => x.Empid == Attence.Employeeid).FirstOrDefaultAsync();

            if (empDtl != null && empDtl.Active && empDtl.Deviceviolation < 3)
            {
                Qry = $"SELECT * FROM ATTENDANCEDETAILS WHERE EMPLOYEEID='{Attence.Employeeid}' AND CONVERT(DATE,ATTENDANCEDDATE,103) = CONVERT(DATE,GETDATE(),103)";
                var empData = await this._dbContext.Attendancedetails.FromSqlRaw(Qry).FirstOrDefaultAsync();

                if (empData == null)
                {
                    Qry = $"INSERT INTO ATTENDANCEDETAILS (EMPLOYEEID, ISPRESENT, CLIENTPLACE, ATTENDANCEDBY, ATTENDANCEDDATE) VALUES ('{Attence.Employeeid}', '{Attence.Ispresent}', '{Attence.Clientplace}', '{Attence.Attendancedby}', GETDATE())";
                    try
                    {
                        await this._dbContext.Database.ExecuteSqlRawAsync(Qry);
                        return new Api() { result = "Attendance Entry Success.", data = Attence };
                    }
                    catch (Exception ex)
                    {
                        return new Api() { result = "Exception in Attendance.", data = ex };
                    }                    
                }
                else
                {
                    return new Api() { result = "Attendance Already Done For Today.", data = empData };
                }
            }
            else
            {
                return new Api() { result = "Employee Not Found / Device or Employee's are Not Active.", data = empDtl };
            }
        }

    }
}