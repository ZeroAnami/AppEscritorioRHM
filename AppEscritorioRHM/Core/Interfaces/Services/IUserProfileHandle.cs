using AppEscritorioRHM.Core.Entities;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces.Services
{
    public interface IUserProfileHandle
    {
        Task<ContextResult> LoginAsync(string username, string password);
        void Logout();
        Task<ContextResult> RegisterUserAsync(string username, string password);
        Task<ContextResult> RemoveUserAsync(string username, string password);
        bool IsLoggedIn();
        string GetUserName();
        Task<ContextResult> ValidatePassword(string password);
        ProjectConfiguration? GetProjectSelected();
        List<ProjectConfiguration> GetProjects();
        Task<ContextResult> SetProjectSelectedAsync(string projectId);
        Task<ContextResult> AddProjectAsync(ProjectConfiguration project, string password);
        Task<ContextResult> UpdateProjectAsync(ProjectConfiguration project, string password);
        Task<ContextResult> RemoveProjectAsync(string projectId, string password);
        Task<ContextResult> TestConnectionWithEcommerce();
    }
}
