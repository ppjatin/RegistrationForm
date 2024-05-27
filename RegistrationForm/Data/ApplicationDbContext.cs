using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RegistrationForm.Models;

namespace RegistrationForm.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<UserRegistration> UserRegistrations { get; set; }

        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

    }
}