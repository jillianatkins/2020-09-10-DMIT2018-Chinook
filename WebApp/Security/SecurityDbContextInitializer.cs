using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#region Additional Namespaces
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using System.Data.Entity;
using WebApp.Models;
#endregion

namespace WebApp.Security
{
    #region TODO #3 Create the SecurityDbContextInitializer class
    // This class will work with the ApplicationDbContext class to "seed" the database
    // when it generates the database tables if they do not exist.
    public class SecurityDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {
            #region Phase A - Set up our Security Roles
            // 1. Instantiate a Controller class from ASP.Net Identity to add roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            // 2. Grab our list of security roles from the web.config
            var startupRoles = ConfigurationManager.AppSettings["startupRoles"].Split(';');
            // 3. Loop through and create the security roles
            foreach (var role in startupRoles)
                roleManager.Create(new IdentityRole { Name = role });
            #endregion

            #region Phase B - Add a Website Administrator
            // 1. Get the values from the <appSettings>
            string adminUser = ConfigurationManager.AppSettings["adminUserName"];
            string adminRole = ConfigurationManager.AppSettings["adminRole"];
            string adminEmail = ConfigurationManager.AppSettings["adminEmail"];
            string adminPassword = ConfigurationManager.AppSettings["adminPassword"];

            // 2. Instantiate my Controller to manage Users
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            //                \   IdentityConfig.cs    /             \IdentityModels.cs/
            // 3. Add the web admin to the database
            var result = userManager.Create(new ApplicationUser
            {
                UserName = adminUser,
                Email = adminEmail,
                CustomerId = null,
                EmployeeId = null
            }, adminPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(adminUser).Id, adminRole);
            #endregion

            #region Phase C - Add a Customer
            // 1. Get the values from the <appSettings>
            int customerId = int.Parse(ConfigurationManager.AppSettings["customerId"]);
            string customerUser = ConfigurationManager.AppSettings["customerUserName"];
            string customerRole = ConfigurationManager.AppSettings["customerRole"];
            string customerEmail = ConfigurationManager.AppSettings["customerEmail"];
            string customerPassword = ConfigurationManager.AppSettings["customerPassword"];
            result = userManager.Create(new ApplicationUser
            {
                CustomerId = customerId,
                UserName = customerUser,
                Email = customerEmail,
                EmployeeId = null
            }, customerPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(customerUser).Id, customerRole);
            #endregion

            #region Phase D - Add a Employee
            // 1. Get the values from the <appSettings>
            int employeeId = int.Parse(ConfigurationManager.AppSettings["employeeId"]);
            string employeeUser = ConfigurationManager.AppSettings["employeeUserName"];
            string employeeRole = ConfigurationManager.AppSettings["employeeRole"];
            string employeeEmail = ConfigurationManager.AppSettings["employeeEmail"];
            string employeePassword = ConfigurationManager.AppSettings["employeePassword"];
            result = userManager.Create(new ApplicationUser
            {
                EmployeeId = employeeId,
                UserName = employeeUser,
                Email = employeeEmail,
                CustomerId = null
            }, employeePassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(employeeUser).Id, employeeRole);
            #endregion

            base.Seed(context);
        }
    }
    #endregion
}