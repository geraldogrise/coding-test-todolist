using Todo.CrossCutting.AppSetting;
using Microsoft.Extensions.Options;

namespace Todo.Endpoints.Configuration
{
    public static class AppSettingsConfiguration
    {
        public static void Add(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var AcessoOptions = new AccessOptions();
            new ConfigureFromConfigurationOptions<AccessOptions>(configuration.GetSection("AccessConfiguration"))
                .Configure(AcessoOptions);

            services.AddSingleton(AcessoOptions);
        }
    }
}
