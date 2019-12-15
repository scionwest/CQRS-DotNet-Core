using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FocusMark.CQRS
{
    public class HandlerBase
    {
        public IConfiguration Configuration { get; protected set; }

        public IServiceProvider Services { get; protected set; }

        public void InitializeConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            this.ConfigureHandler(configurationBuilder);
            
            this.Configuration = configurationBuilder.Build();
        }

        public void InitializeServiceProvider()
        {
            var services = new ServiceCollection();
            this.RegisterHandlerServices(services);

            this.Services = services.BuildServiceProvider();
        }

        protected virtual void ConfigureHandler(IConfigurationBuilder configurationBuilder)
        {
            string environment = System.Environment.GetEnvironmentVariable("FOCUSMARK_ENVIRONMENT");
            configurationBuilder.AddJsonFile("appsettings.json", optional: true);
            if (!string.IsNullOrEmpty(environment))
            {
                configurationBuilder.AddJsonFile($"appsettings.{environment}.json", optional: false);
            }

            configurationBuilder.AddEnvironmentVariables();
        }

        protected virtual void RegisterHandlerServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(this.Configuration);
        }
    }
}
