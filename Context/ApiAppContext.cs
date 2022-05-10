using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using newWebAPI.Models;

namespace newWebAPI.Context
{
    public class ApiAppContext : DbContext
    {
        public DbSet<User> Users {get;set;}
        public DbSet<UserRole> UserRoles {get;set;}
        public ApiAppContext(DbContextOptions<ApiAppContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("User").HasKey(p => p.UserId);
            builder.Entity<UserRole>().ToTable("UserRole").HasKey(p => p.UserRoleId);

            List<User> initialUsers = new List<User>();
            initialUsers.Add(new User{ Name = "San", LastName = "Suarez" });
            initialUsers.Add(new User{ Name = "Juan", LastName = "Romero" });
            builder.Entity<User>().HasData(initialUsers);

            List<UserRole> initialUserRoles = new List<UserRole>();
            initialUserRoles.Add(new UserRole{ Role = "Admin" , UserId = initialUsers[0].UserId });
            initialUserRoles.Add(new UserRole{ Role = "User" , UserId = initialUsers[1].UserId });
            builder.Entity<UserRole>().HasData(initialUserRoles);

            builder.Entity<UserRole>().HasOne<User>("User");

        }
    }
}