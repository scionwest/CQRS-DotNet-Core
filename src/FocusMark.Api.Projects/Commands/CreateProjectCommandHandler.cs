using FocusMark.Api.Projects.Repositories;
using FocusMark.CQRS;
using FocusMark.Domain.Project;
using FocusMark.EventSource;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;

namespace FocusMark.Api.Projects.Commands
{
    public class CreateProjectCommandHandler : ApiGatewayCommandHandler<CreateProjectCommand>
    {
        private readonly IProjectRepository repository;

        public CreateProjectCommandHandler() : base()
        {
            this.repository = base.Services.GetRequiredService<IProjectRepository>();
        }

        protected override void RegisterHandlerServices(IServiceCollection services)
        {
            base.RegisterHandlerServices(services);
            services.AddTodoServices();
        }

        protected override async Task<HandlerResponse> CommandHandler(CreateProjectCommand requestBody)
        {
            if (!this.ProxyRequest.Headers.TryGetValue("username", out string username))
            {
                username = "janedoe";
            }

            var newProject = new Project(requestBody.Id, "janedoe", requestBody.Priority, requestBody.Title);
            if (requestBody.IsFlagged)
            {
                newProject.FlagProject();
            }
            newProject.SetPercentageComplete(requestBody.PercentageCompleted);

            if (requestBody.StartDate.HasValue)
            {
                newProject.StartProject(requestBody.StartDate.Value);
            }

            if (requestBody.TargetDate.HasValue)
            {
                newProject.SetTargetDate(requestBody.TargetDate.Value);
            }

            try
            {
                base.Logger.LogLine($"Saving {newProject.Title} for {username} into repository.");
                await repository.CreateProjectAsync(username, newProject);

                //string streamName = this.Configuration["AWS:EventSource:StreamName"];
                //var bus = new EventBus();
                //var record = new EventRecord(1, newProject, streamName);

                //base.Logger.LogLine($"Publishing {newProject.Title} for {username} into {streamName} event source.");
                //await bus.PublishMessage(record);
                return this.StatusCreated(newProject.Id);
            }
            catch (System.Exception ex)
            {
                this.Logger.LogLine(ex.Message);
                return new HandlerResponse((int)HttpStatusCode.BadRequest);
            }
        }
    }
}
