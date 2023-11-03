
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserMangement.Repositories.Context.Entities;
using UserMangement.Services.Abstraction.Model;

namespace Seasia.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
      
        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserModel userModel)
        {
            var IsExist = await userManager.FindByEmailAsync(userModel.Email);

            if (IsExist != null) return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVm
            {
                Status = "Error",
                Message = "User already exists!"
            });

            User user = new User
            {
                Firstname=userModel.Firstname,
                Lastname=userModel.Lastname,
                Address=userModel.Address,
                UserName=userModel.Username,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber,         
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            if (!result.Succeeded) return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVm
            {
                Status = "Error",
                Message = "User creation failed! Please check user details and try again."
            });

            if (!await roleManager.RoleExistsAsync(userModel.UserRole)) await roleManager.CreateAsync(new IdentityRole(userModel.UserRole));

            if (await roleManager.RoleExistsAsync(userModel.UserRole))
            {
                await userManager.AddToRoleAsync(user, userModel.UserRole);
            }
            return Ok(new ResponseVm
            {
                Status = "Success",
                Message = "User created successfully!"
            });
            }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
          
            var user = await userManager.FindByNameAsync(loginVM.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, loginVM.Password))
            {
              
                var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
                
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Tokens:Issuer"],
                    audience: _configuration["Tokens:Audience"],
                    claims: authClaims,
                    expires: DateTime.UtcNow.AddDays(int.Parse(_configuration["Tokens:Expiration"])),
                    signingCredentials: creds);

                return Ok(new
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(token),
                    token_type = "bearer",
                    expires_in = (token.ValidTo - DateTime.Today).TotalSeconds
                });
            }
                  return this.Unauthorized("Incorrect Username or password");
        }


        

    }



}
