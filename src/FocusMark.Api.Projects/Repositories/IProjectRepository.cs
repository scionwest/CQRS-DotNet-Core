﻿using FocusMark.Domain.Project;
using System.Threading.Tasks;

namespace FocusMark.Api.Projects.Repositories
{
    public interface IProjectRepository
    {
        Task CreateProjectAsync(string username, Project newProject);
        Task DeleteProjectAsync(string username, string projectId);

        Task<Project[]> GetProjectsForUserAsync(string username);
        Task<Project> GetProjectByIdForUserAsync(string username, string projectId);
    }
}
