using MediatR;
using Todo.CrossCuttingIoc;
using Todo.Endpoints.Middleware;


namespace Todo.Endpoints.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            BootStrapper.RegisterServices(services);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MediatorMiddleware<,>));
        }
    }
}
