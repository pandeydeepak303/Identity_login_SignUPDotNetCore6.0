using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMangement.Services.Abstraction.Model
{
    public class UserModel
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserRole { get; set; }
        public string  Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

    }
}
