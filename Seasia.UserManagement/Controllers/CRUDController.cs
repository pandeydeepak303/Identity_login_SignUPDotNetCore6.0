using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserMangement.Services.Abstraction.Abstraction;
using UserMangement.Services.Abstraction.Model;

namespace Seasia.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly IUserService _userService;
       
        public CRUDController( IUserService userService )
        {
            _userService = userService;
          
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IdentityUser>> GetUseryId(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(UserModel userModel)
        {

            var user = await _userService.AddUserAsync(userModel);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser( [FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedUser =  _userService.UpdateUserAsync( userModel);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    

    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
          
            if (id != null)
            {
                await _userService.DeleteUserAsync(id);
                return Ok("Deleted Successfully");
            }
            else
            {
               return BadRequest("Something went wrong");
            }
          
        }
    }
}
