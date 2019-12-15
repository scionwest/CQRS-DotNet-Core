using FocusMark.Api.Projects.Repositories;
using FocusMark.CQRS;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FocusMark.Api.Projects.Commands
{
    public class DeleteProjectCommandHandler : ApiGatewayCommandHandler
    {
        private readonly IProjectRepository repository;

        public DeleteProjectCommandHandler() : base()
        {
            this.repository = base.Services.GetRequiredService<IProjectRepository>();
        }

        protected override void RegisterHandlerServices(IServiceCollection services)
        {
            base.RegisterHandlerServices(services);
            services.AddTodoServices();
        }

        protected override async Task<HandlerResponse> CommandHandler()
        {
            if (!this.ProxyRequest.Headers.TryGetValue("username", out string username))
            {
                username = "janedoe";
            }

            string projectId = base.ProxyRequest.PathParameters["projectId"];
            await repository.DeleteProjectAsync(username, projectId);

            return this.StatusDeleted(projectId);
        }
    }
}
