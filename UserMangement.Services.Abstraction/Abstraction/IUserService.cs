using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMangement.Services.Abstraction.Model;

namespace UserMangement.Services.Abstraction.Abstraction
{
    public interface IUserService
    {
        Task<UserModel> GetUserByIdAsync(string id);
        Task<IdentityResult> AddUserAsync(UserModel userModel);
        Task<IdentityResult> UpdateUserAsync( UserModel userModel);
        Task DeleteUserAsync(string id);

    }
}
