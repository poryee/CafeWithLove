namespace CafeWithLove.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CafeWithLove.DAL.CafeWithLoveContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CafeWithLove.DAL.CafeWithLoveContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //var userStore = new UserStore<ApplicationUser>(context);
            //var userManager = new UserManager<ApplicationUser>(userStore);
            //var user = context.Users.Where(t => t.UserName.Equals("admin")).First();
            //context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Admin" });
            //context.SaveChanges();
            //userManager.AddToRole(user.Id, "Admin");
        }
    }
}
