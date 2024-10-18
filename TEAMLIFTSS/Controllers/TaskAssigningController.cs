using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEAMLIFTSS.Repos.TableModels;
using TEAMLIFTSS.Services.IServices;

namespace TEAMLIFTSS.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class TaskAssigningController : ControllerBase
    {
        private readonly ITaskAssigning _service;
        public TaskAssigningController(ITaskAssigning taskService)
        {
            this._service = taskService;
        }

        [Route("api/Tasks")]
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await this._service.GetTaskList();
            return Ok(tasks);
        }


        [Route("api/AssainingTask")]
        [HttpPost]
        public async Task<IActionResult> PostTask(Taskdetail taskDtl)
        {
            if (taskDtl != null)
            {
                var tasks = await this._service.AssigningTasks(taskDtl);
                return Ok(tasks);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
