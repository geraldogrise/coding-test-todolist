using Microsoft.Extensions.Options;
using Todo.CrossCutting.Log;

namespace Todo.Endpoints.Configuration
{
    public static class LogConfiguration
    {
        public static void AddLoggerService(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var LoggerOptions = new LoggerOptions();
            new ConfigureFromConfigurationOptions<LoggerOptions>(configuration.GetSection("LogOptions"))
                .Configure(LoggerOptions);

            services.AddSingleton(LoggerOptions);
        }
    }
}
