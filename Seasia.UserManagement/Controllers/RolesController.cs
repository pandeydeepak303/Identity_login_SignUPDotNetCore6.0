using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Seasia.UserManagement.Controllers
{


    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("Api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;


        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

  
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(string name)
        {
            var role = await _roleManager.RoleExistsAsync(name);
            if (!role)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole { Name = name });

                return Ok(result);
            }
            else
            {
                return Ok("This role already exist");
            }
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(result.Errors);
            }
            return NotFound();
        }
    }
}
