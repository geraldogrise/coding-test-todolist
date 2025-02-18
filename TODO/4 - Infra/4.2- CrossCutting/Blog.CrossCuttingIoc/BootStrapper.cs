using MediatR;
using Todo.Data.Context;
using Todo.Data.Repository;
using Todo.Application.App;
using Todo.CrossCutting.Log;
using Todo.Domain.Notifications;
using Todo.Application.Interfaces;
using Todo.Domain.Aggreagates.Tasks.Services;
using Todo.Domain.Aggreagates.Users.Services;
using Todo.Domain.Aggreagates.Users.Repository;
using Todo.Domain.Aggreagates.Tasks.Repository;
using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Aggreagates.Auth;



namespace Todo.CrossCuttingIoc
{
    public static class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<ITaskAppService, TaskAppService>();

            // Domain
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Service
            services.AddSingleton<ILogger, Logger>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskService, TasltService>();

            // Data
            services.AddScoped<DatabaseContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }
}
