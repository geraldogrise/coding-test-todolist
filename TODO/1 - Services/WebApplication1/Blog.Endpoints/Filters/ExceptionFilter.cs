using Microsoft.AspNetCore.Mvc.Filters;
using Todo.CrossCutting.Log;

namespace Todo.Endpoints.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly Todo.CrossCutting.Log.ILogger _logger;

        public ExceptionFilter(Todo.CrossCutting.Log.ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                _logger.writeLog (context.Exception);
            }
        }
    }
}
