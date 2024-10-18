using Microsoft.EntityFrameworkCore;
using TEAMLIFTSS.Repos;
using TEAMLIFTSS.Repos.TableModels;
using TEAMLIFTSS.Services.IServices;

namespace TEAMLIFTSS.Services
{
    public class TaskAssigning : ITaskAssigning
    {
        private readonly ApplicationDBContext _dbContext;
        public TaskAssigning(ApplicationDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        //public async Task<dynamic> AssigningTasks(Taskdetail Taskdtls)
        //{
        //    try
        //    {
        //        string Qry = $"SELECT COUNT(*) AS EMPCOUNT FROM EMPLOYEEDETAILS WHERE EMPID IN ('{Taskdtls.Taskassigners}','{Taskdtls.Taskreceiver}') AND ACTIVE = 1";
        //        //var userActive = await this._dbContext.Database.ExecuteSqlRawAsync(Qry);
        //        var userActive = await this._dbContext.Employeedetails.Where(x => x.Empid == Taskdtls.Taskassigners || x.Empid == Taskdtls.Taskreceiver && x.Active == true).CountAsync();
        //        if (Convert.ToInt32(userActive) == 2)
        //        {
        //            await this._dbContext.Taskdetails.AddAsync(Taskdtls);
        //            await this._dbContext.SaveChangesAsync();
        //            return new { Result = "Task Assaigned Successfully.", ResponseCode = 200, data = Taskdtls };
        //        }
        //        else
        //        {
        //            return new { Result = "Employee Not Availabel / Active ", ResponseCode = 204, data = Taskdtls };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new { Result = "Exception .", ResponseCode = 400, data = ex.Message };
        //    }
        //}
        public async Task<dynamic> AssigningTasks(Taskdetail Taskdtls)
        {
            try
            {
                // Ensure that existing EMPLOYEEDETAILS entries are not tracked again
                var assigner = await this._dbContext.Employeedetails
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Empid == Taskdtls.Taskassigners && x.Active == true);

                var receiver = await this._dbContext.Employeedetails
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Empid == Taskdtls.Taskreceiver && x.Active == true);

                if (assigner != null && receiver != null)
                {
                    // Add task details without re-adding employees
                    this._dbContext.Taskdetails.Add(Taskdtls);

                    // Save changes to the Taskdetails table
                    await this._dbContext.SaveChangesAsync();

                    return new { Result = "Task Assigned Successfully.", ResponseCode = 200, data = Taskdtls };
                }
                else
                {
                    return new { Result = "Employee Not Available / Active", ResponseCode = 204, data = Taskdtls };
                }
            }
            catch (Exception ex)
            {
                return new { Result = "Exception occurred.", ResponseCode = 400, data = ex.Message };
            }
        }



        public async Task<dynamic> GetTaskList()
        {
            try
            {
                var resdata = await this._dbContext.Taskdetails.ToListAsync();
                return new { Result = resdata.Count > 0 ? "Success." : "Error.", ResponseCode = resdata.Count > 0 ? 200 : 400, data = resdata };
            }
            catch (Exception ex)
            {
                return new { Result = "Exception .", ResponseCode = 400, data = ex.Message };
            }
        }
    }
}
