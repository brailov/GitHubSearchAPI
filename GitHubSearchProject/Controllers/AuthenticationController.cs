using GitHubSearchProject.DTOs;
using GitHubSearchProject.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace GitHubSearchProject.Controllers
{    
    [ApiController]
    public class AuthenticationController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("api/authentication/login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.Login(loginDTO);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
