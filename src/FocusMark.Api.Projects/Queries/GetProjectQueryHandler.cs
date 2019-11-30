using FocusMark.Api.Projects.Repositories;
using FocusMark.CQRS;
using FocusMark.Domain.Project;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FocusMark.Api.Projects.Queries
{
    public class GetProjectQueryHandler : ApiGatewayQueryHandler
    {
        protected override Task RegisterHandlerServices(IServiceCollection services)
        {
            services.AddTodoServices();
            return base.RegisterHandlerServices(services);
        }

        protected override async Task<HandlerResponse> QueryHandler()
        {
            IProjectRepository repository = base.Services.GetRequiredService<IProjectRepository>();

            if (!this.ProxyRequest.Headers.TryGetValue("username", out string username))
            {
                username = "janedoe";
            }
            string projectId = base.ProxyRequest.PathParameters["projectId"];

            Project project = await repository.GetProjectByIdForUserAsync(username, projectId);
            var result = new GetProjectQuery(project);

            return this.StatusOk(result);
        }
    }
}
