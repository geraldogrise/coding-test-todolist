using MediatR;

namespace Todo.Endpoints.Middleware
{
    public class MediatorMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IHttpContextAccessor _httpContext;

        public MediatorMiddleware(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = next();
            return response;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = next();
            return response;
        }
    }
}
