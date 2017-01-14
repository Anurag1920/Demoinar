﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace Webinar.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("OnlineTestConnection")
        {
        }
    }

    public class ApplicationRoles : IdentityRole
    {

    }

    public class UserRole : IdentityUserRole
    {

    }
}