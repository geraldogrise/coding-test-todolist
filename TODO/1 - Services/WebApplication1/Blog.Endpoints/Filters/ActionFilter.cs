using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Todo.Domain.Aggreagates.Core;

namespace Todo.Endpoints.Filters
{
    public class ActionFilter : IActionFilter
    {
        private readonly Token _token;
        private readonly ILogger _logger;

        public string Parametros { get; set; }

        public ActionFilter(Token token, ILogger<ActionFilter> logger)
        {
            _token = token;
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                _logger.LogError(context.Exception, "Ocorreu um erro. Parâmetros de entrada: {Parametros}", Parametros);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Parametros = JsonConvert.SerializeObject(context.ActionArguments);
        }
    }
}
