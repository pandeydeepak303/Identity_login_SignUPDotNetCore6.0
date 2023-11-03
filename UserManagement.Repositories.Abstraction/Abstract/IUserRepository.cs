using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMangement.Repositories.Context.Entities;

namespace UserManagement.Repositories.Abstraction.Abstract
{
    public  interface IUserRepository 
    {
        Task<User> GetByIdAsync(string id );
        Task AddAsync(User user);
        Task <User>UpdateAsync(  User user);
        Task DeleteAsync(string id );

       
     
    }
}
