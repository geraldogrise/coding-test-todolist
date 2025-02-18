using Todo.Application.Interfaces;
using Todo.Application.ViewModels;
using Todo.Data.Core.Repository;
using Todo.Domain.Aggreagates.Tasks;
using Todo.Domain.Notifications;
using Todo.Endpoints.Controllers.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Endpoints.Controllers
{

    [Route("api/tasks")]
    [ApiController]
    public class TaskController : BaseController
    {
        private readonly ITaskAppService _taskAppService;

        public TaskController(INotificationHandler<DomainNotification> notifications,
                                               IMediator mediator,
                                               ITaskAppService taskAppService) : base(notifications, mediator)
        {
            _taskAppService = taskAppService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Response(_taskAppService.GetAll());
            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                return Response(null, ex.Message, null);
            }

        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                var task = _taskAppService.GetById(id);
                if (task == null)
                    return NotFound(new { success = false, message = "Task not found" });
                return Response(task);
            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                return Response(null, ex.Message, null);
            }
        }


        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] TaskModel taskModel)
        {
            try
            {
                var task = _taskAppService.Insert(taskModel);
                return Ok(new { success = true, message = "Task inserted successfully", data = task });

            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                return Response(null, ex.Message, null);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaskModel taskModel)
        {
            try
            {
                var updatedTask = _taskAppService.Update(id, taskModel);
                if (updatedTask == null)
                    return NotFound(new { success = false, message = "Task not found" });
                return Ok(new { success = true, message = "Task updated successfully", data = updatedTask });
            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                return Response(null, ex.Message, null);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var success = _taskAppService.Delete(id);
                if (!success)
                    return NotFound(new { success = false, message = "Task not found" });
                return Ok(new { success = true, message = "Task deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("all")]
        public IActionResult GetTasks()
        {
            try
            {
                var tasks = _taskAppService.GetTasks();
                if (tasks.Count == 0)
                    return NotFound(new { success = false, message = "Tasks not found" });
                return Response(tasks);

            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
