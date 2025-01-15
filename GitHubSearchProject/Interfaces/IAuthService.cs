using GitHubSearchProject.DTOs;

namespace GitHubSearchProject.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginDTO loginDTO);
    }
}
