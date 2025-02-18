using MediatR;
using Todo.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Notifications;
using Todo.Application.Interfaces;
using Todo.Application.ViewModels;
using Todo.Endpoints.Controllers.Core;



namespace Todo.Endpoints.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IUserAppService _userAppService;
        public AuthController(INotificationHandler<DomainNotification> notifications,
                                              IMediator mediator,
                                              IUserAppService userAppService) : base(notifications, mediator)
        {
            _userAppService = userAppService;
        }

        [HttpPost("Signin")]
        public IActionResult Signin([FromBody] LoginModel loginModel)
        {
            try
            {
                string token = _userAppService.Signin(loginModel);
                return Ok(new { success = true, token });
            }
            catch (AuthenticationExcecption ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while processing the request. Please try again later " + ex.Message });
            }
           
        }
    }
}
