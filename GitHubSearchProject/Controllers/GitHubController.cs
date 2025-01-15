using GitHubSearchProject.DTOs;
using GitHubSearchProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GitHubSearchProject.Controllers
{
    [Authorize] // Restrict access to authenticated users  
    [ApiController]
    public class GitHubController(IGitRepositoriesService gitRepositoriesService) : ControllerBase
    {
        private readonly IGitRepositoriesService _gitRepositoriesService = gitRepositoriesService;

        [HttpPost("api/git-hub/repositories")]
        public async Task<IActionResult> GetRepositoriesAsync([FromBody]SearchDTO searchDTO)
        {
            //check model state is all data annotrations are passed, we don't want to search for empty string
            if (!ModelState.IsValid)
                return BadRequest();

            var repos = await _gitRepositoriesService.GetRepositories(searchDTO.KeyWord);
            return Ok(repos);
        }
    }
}
