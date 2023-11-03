using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seasia.UserManagement.MigrationProject.Entitites;
using System.Reflection.Emit;
using UserMangement.Repositories.Context.Entities;

namespace Seasia.UserManagement.MigrationProject
{
    public class SeasiaUserContext : IdentityDbContext<IdentityUser>
    {
        public SeasiaUserContext()
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-MNV89QG\\SQLEXPRESS;Database=Seasia_UserInfo;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
        }
    }
}
