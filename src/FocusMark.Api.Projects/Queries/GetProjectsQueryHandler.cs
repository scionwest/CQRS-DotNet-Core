using FocusMark.Api.Projects.Repositories;
using FocusMark.CQRS;
using FocusMark.Domain.Project;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FocusMark.Api.Projects.Queries
{
    public class GetProjectsQueryHandler : ApiGatewayQueryHandler
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

            Project[] projects = await repository.GetProjectsForUserAsync(username);
            GetProjectsQuery result = new GetProjectsQuery(projects);
            return this.StatusOk(result);
        }
    }
}
