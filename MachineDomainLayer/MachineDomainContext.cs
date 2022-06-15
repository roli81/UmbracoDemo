using MachineDomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineDomainLayer
{
    public class MachineDomainContext : DbContext
    {

        public DbSet<Machine> Machines { get; set; }
        public DbSet<Metric> Metrics { get; set; }  


        public MachineDomainContext(DbContextOptions<MachineDomainContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Machine>().ToTable("Machines");
            modelBuilder.Entity<Metric>().ToTable("Metrics");
        }

    }
}
