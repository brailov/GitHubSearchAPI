using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GitHubSearchAPI.Controllers
{
    [Authorize] // Restrict access authenticated users  
    [ApiController]
    public class UserController : ControllerBase
    {
        
        [HttpGet("api/user/menu")]
        public async Task<IActionResult> GetUserMenu()
        {
            List<MenuItem> menus = [
                new MenuItem("mybookmarks", "My Bookmarks")
            ];
            return Ok(menus);
        }      
    }
}
