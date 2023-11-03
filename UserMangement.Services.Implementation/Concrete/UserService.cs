using Microsoft.AspNetCore.Identity;
using UserManagement.Repositories.Abstraction.Abstract;
using UserMangement.Repositories.Context.Entities;
using UserMangement.Services.Abstraction.Abstraction;
using UserMangement.Services.Abstraction.Model;

namespace UserMangement.Services.Implementation.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public UserService(IUserRepository userRepository   ,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

           _userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;

        }

        public async Task<IdentityResult> AddUserAsync(UserModel userModel)
        {
            User user = new User
            {
                Firstname = userModel.Firstname,
                Lastname = userModel.Lastname,
                Address = userModel.Address,
                UserName = userModel.Username,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber,
            };
            var result = await userManager.CreateAsync(user, userModel.Password);

            if (!await roleManager.RoleExistsAsync(userModel.UserRole)) await roleManager.CreateAsync(new IdentityRole(userModel.UserRole));

            if (await roleManager.RoleExistsAsync(userModel.UserRole))
            {
                await userManager.AddToRoleAsync(user, userModel.UserRole);
            }
            return result;

        }

        public  async Task DeleteUserAsync(string id )
        {
            if ( id == null)
            {

                throw new Exception($"User with ID {id} not found.");
            }
            await _userRepository.DeleteAsync(id);    
        }


        public Task<UserModel> GetUserByIdAsync(string id)
        {

            return null;
     
        }


        public  async Task<IdentityResult> UpdateUserAsync(  UserModel userModel)
        {
           
          
            User user = new User
            {
              
                Firstname = userModel.Firstname,
                Lastname = userModel.Lastname,
                Address = userModel.Address,
                UserName = userModel.Username,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber,
            };
            var result = await userManager.UpdateAsync(user);

                       await roleManager.UpdateAsync(new IdentityRole(userModel.UserRole));

            return result;

        }
      
    }
}
