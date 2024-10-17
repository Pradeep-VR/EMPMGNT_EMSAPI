using Microsoft.EntityFrameworkCore;
using TEAMLIFTSS.Repos;
using TEAMLIFTSS.Repos.TableModels;
using TEAMLIFTSS.Services.IServices;

namespace TEAMLIFTSS.Services
{
    public class TaskAssigning : ITaskAssigning
    {
        private readonly ApplicationDBContext _dbContext ;
        public TaskAssigning(ApplicationDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task<dynamic> AssigningTasks(Taskdetail Taskdtls)
        {
            throw new NotImplementedException();
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
