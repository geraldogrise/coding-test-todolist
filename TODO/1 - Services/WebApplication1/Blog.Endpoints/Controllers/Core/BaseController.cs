using Todo.Domain.Messages;
using Todo.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Endpoints.Controllers.Core
{
    public class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediator _mediator;

        public BaseController(INotificationHandler<DomainNotification> notifications, IMediator mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected new IActionResult Response()
        {
            return Response(null, null, null);
        }

        protected new IActionResult Response<T>(Task<T> task)
        {
            if (task.Result is Unit)
                return Response(task, null);

            return Response(task, task.Result);
        }

        protected new IActionResult Response(object result)
        {
            return Response(null, result, null);
        }

        protected new IActionResult Response(Task task, object result = null)
        {
            return Response(task, result, null);
        }

        protected new IActionResult Response(Task task, object result, object validationMessage, object type = null)
        {
            if (IsValidOperation() && IsValidResponse(task))
            {
                if (validationMessage == null)
                    validationMessage = _notifications.GetInformations().Select(n => n.Value);

                return Ok(new
                {
                    success = true,
                    informations = validationMessage,
                    data = result
                });
            }
            else
            {
                var message = _notifications.GetErrors().Select(n => n.Value).Distinct();

                if (type == null)
                {
                    type = "error";
                }
                return BadRequest(new
                {
                    success = false,
                    message = message.First(),
                    type
                });
            }
        }

        protected bool IsValidResponse(Task task)
        {
            if (task != null && task.Exception != null)
            {
                _mediator.Publish(new DomainNotification(NotificationType.Error, "Task", Mensagens.OperacaoInvalida));
                return false;
            }

            return true;
        }

        protected bool IsValidOperation()
        {
            return !_notifications.HasError();
        }

        protected void RaiseErrorModel()
        {
            var erros = ModelState.Values.SelectMany(x => x.Errors);

            foreach (var erro in erros)
                _mediator.Publish(new DomainNotification(NotificationType.Error, "ViewModel", erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message));
        }

        protected void RaiseError(string error)
        {
            _mediator.Publish(new DomainNotification(NotificationType.Error, "ViewModel", error));
        }

        protected virtual bool ModelStateIsValid()
        {
            if (ModelState.IsValid) return true;

            RaiseErrorModel();
            return false;
        }
    }
}
