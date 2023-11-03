using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMangement.Repositories.Context.Entities
{
    public class User : IdentityUser
    {
       
        public string? Firstname { get; set; } 
        public string? Lastname { get; set; } 
        public string?  Address { get; set; }

     
      
     
    }
}
