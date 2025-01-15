using GitHubSearchProject.DTOs;

namespace GitHubSearchProject.Interfaces
{
    public interface IGitRepositoriesService
    {
        Task<List<GitRepository>> GetRepositories(string keyWord);
    }
}
