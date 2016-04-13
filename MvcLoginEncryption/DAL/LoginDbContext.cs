using MvcLoginEncryption.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcLoginEncryption.DAL
{
    public class LoginDbContext:DbContext
    {
        public LoginDbContext()
            : base("name=DbConnectionString")
        { }

        public DbSet<User> Users { get; set; }
    }
}