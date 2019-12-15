using FocusMark.Api.Projects.Repositories;
using FocusMark.CQRS;
using FocusMark.Domain.Project;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FocusMark.Api.Projects.Queries
{
    public class GetProjectQueryHandler : ApiGatewayQueryHandler
    {
        private readonly IProjectRepository repository;

        public GetProjectQueryHandler() : base()
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
            string projectId = base.ProxyRequest.PathParameters["projectId"];

            Project project = await repository.GetProjectByIdForUserAsync(username, projectId);
            var result = new GetProjectQuery(project);

            return this.StatusOk(result);
        }
    }
}
