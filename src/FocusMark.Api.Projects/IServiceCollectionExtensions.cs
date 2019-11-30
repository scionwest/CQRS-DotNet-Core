using Amazon.DynamoDBv2;
using FocusMark.Api.Projects.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FocusMark.Api.Projects
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTodoServices(this IServiceCollection services)
        {
            services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
            services.AddSingleton<IProjectRepository, ProjectDynamoRepository>();

            return services;
        }
    }
}
