using FocusMark.Domain.Project;
using System.Linq;

namespace FocusMark.Api.Projects.Queries
{
    public class GetProjectsQuery
    {
        public GetProjectsQuery(Project[] projects)
        {
            this.Projects = projects
                .Select(project => new GetProjectQuery(project))
                .ToArray();
        }

        public GetProjectQuery[] Projects { get; }
    }
}
