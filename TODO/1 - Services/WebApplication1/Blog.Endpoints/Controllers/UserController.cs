using Todo.Application.App;
using Todo.Application.Interfaces;
using Todo.Application.ViewModels;
using Todo.Data.Core.Repository;
using Todo.Domain.Aggreagates.Users;
using Todo.Domain.Notifications;
using Todo.Endpoints.Controllers.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Endpoints.Controllers
{

    [Route("api/users")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserAppService _userAppService;
        private readonly ITaskAppService _postAppService;

        public UserController(INotificationHandler<DomainNotification> notifications,
                                               IMediator mediator,
                                               ITaskAppService postAppService,
                                               IUserAppService userAppService) : base(notifications, mediator)
        {
            _userAppService = userAppService;
            _postAppService = postAppService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Response(_userAppService.GetAll());
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
                var user = _userAppService.GetById(id);
                if (user == null)
                    return NotFound(new { success = false, message = "User not found" });
                return Response(user);
            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                return Response(null, ex.Message, null);
            }
        }


        [HttpPost]
        public IActionResult Create([FromBody] UserModel userModel)
        {
            try
            {
                var user = _userAppService.Insert(userModel);
                return Ok(new { success = true, message = "User inserted successfully", data = user });

            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                return Response(null, ex.Message, null);
            }
        }


        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserModel userModel)
        {
            try
            {
                var updatedUser = _userAppService.Update(id, userModel);
                if (updatedUser == null)
                    return NotFound(new { success = false, message = "User not found" });
                return Ok(new { success = true, message = "User updated successfully", data = updatedUser });
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
                var success = _userAppService.Delete(id);
                if (!success)
                    return NotFound(new { success = false, message = "User not found" });
                return Ok(new { success = true, message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}/tasks")]
        public IActionResult GetByUser(int id)
        {

            try
            {
                var post = _postAppService.GetByUser(id);
                if (post == null)
                    return NotFound(new { success = false, message = "Tasks not found" });
                return Response(post);
            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
                return Response(null, ex.Message, null);
            }
        }
    }
}
