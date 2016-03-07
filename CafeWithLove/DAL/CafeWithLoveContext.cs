using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CafeWithLove.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CafeWithLove.DAL
{
    public class CafeWithLoveContext : IdentityDbContext<ApplicationUser>
    {
        public CafeWithLoveContext() : base("CafeWithLoveDB")
        {

        }
        public DbSet<CafeDetail> CafeDetails { get; set; }
        public DbSet<CafeOutlet> CafeOutlets { get; set; }
    }
}