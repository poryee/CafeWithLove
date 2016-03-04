using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CafeWithLove.Models;

namespace CafeWithLove.DAL
{
    public class CafeWithLoveContext: DbContext
    {
        public CafeWithLoveContext(): base("CafeWithLoveDB"){

        }
        public DbSet<CafeDetail> CafeDetails { get; set; }
        public DbSet<CafeOutlet> CafeOutlets { get; set; }
    }
}