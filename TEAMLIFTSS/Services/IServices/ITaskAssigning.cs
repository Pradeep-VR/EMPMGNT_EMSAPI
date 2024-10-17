using TEAMLIFTSS.Models;
using TEAMLIFTSS.Repos.TableModels;

namespace TEAMLIFTSS.Services.IServices
{
    public interface ITaskAssigning
    {
        public Task<dynamic> AssigningTasks(Taskdetail Taskdtls);

        public Task<dynamic> GetTaskList();
    }
}
