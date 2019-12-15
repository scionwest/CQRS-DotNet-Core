using FocusMark.Api.Projects.Repositories;
using FocusMark.CQRS;
using FocusMark.Domain.Project;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FocusMark.Api.Projects.Queries
{
    public class GetProjectsQueryHandler : ApiGatewayQueryHandler
    {
        private readonly IProjectRepository repository;

        public GetProjectsQueryHandler() : base()
        {
            this.repository = base.Services.GetRequiredService<IProjectRepository>();
        }

        protected override void RegisterHandlerServices(IServiceCollection services)
        {
            base.RegisterHandlerServices(services);
            services.AddTodoServices();
        }

        protected override async Task<HandlerResponse> QueryHandler()
        {
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
