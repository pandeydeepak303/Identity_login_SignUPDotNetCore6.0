using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Seasia.UserManagement.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    { 
        private readonly UserManager<IdentityUser> userManager;    
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
            [HttpGet]
            public async Task<ActionResult<IEnumerable<IdentityUser>>> GetAllUsers()
            {
               var data  =  await userManager.Users.ToListAsync();

                    return Ok(data);
            }
        
    }
}
