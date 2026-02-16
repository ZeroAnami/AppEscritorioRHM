using AppEscritorioRHM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Core.Interfaces.Infrastructure
{
    public interface IUserConnecction
    {
        Task<(ContextResult Result, UserProfile? User)> GetUserAsync(string username, string password);
        Task<ContextResult> UpdateUserNameAsync(string oldUsername, string newUserName, string password);
        Task<ContextResult> RegisterUserAsync(string username, string password);
        Task<ContextResult> RemoveUserAsync(string username, string password);
        Task<ContextResult> VerifyCredentialsAsync(string username, string password);
        Task<ContextResult> SetProjectSelected(UserProfile user, string projectId);
        Task<ContextResult> AddProject(UserProfile user, ProjectConfiguration project, string password);
        Task<ContextResult> UpdateProject(UserProfile user, ProjectConfiguration project, string password);
        Task<ContextResult> RemoveProjectAsync(UserProfile user, string projectId, string password);
    }
}
