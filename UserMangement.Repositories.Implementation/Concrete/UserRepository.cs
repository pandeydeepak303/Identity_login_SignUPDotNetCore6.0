
using Microsoft.AspNetCore.Identity;
using UserManagement.Repositories.Abstraction.Abstract;
using UserMangement.Repositories.Context;
using UserMangement.Repositories.Context.Entities;

namespace UserMangement.Repositories.Implementation.Concrete
{

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> userManager;
        public UserRepository(UserManager<IdentityUser> userManager,ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
             this.userManager = userManager;
          
        }

        public async Task AddAsync(User user)
        {

            var data = userManager.CreateAsync(user);

            if (data != null)
            {
                await userManager.CreateAsync(user, user.PasswordHash);
            }

        }

        public async Task DeleteAsync(string id)
        {
            try
            {

                if (id == null)
                {
                    throw new Exception($"User with ID {id} not found.");
                }
                else
                {
                    var user = _applicationDbContext.Users.SingleOrDefault(x => x.Id == id);
                    _applicationDbContext.Users.Remove(user);
                    await _applicationDbContext.SaveChangesAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<User> GetByIdAsync(string id)
        {
            var data = _applicationDbContext.Users.SingleOrDefault(s => s.Id == id);

            return data;

        }



        public async Task<User> UpdateAsync(User user)
        {
           
            try
            {
                var result = _applicationDbContext.Update(user);

                await _applicationDbContext.SaveChangesAsync();


                return user;
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }
    }
}
