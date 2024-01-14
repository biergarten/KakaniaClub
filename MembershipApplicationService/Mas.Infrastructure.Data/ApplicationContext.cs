using Mas.Domain.Aggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas.Infrastructure.Data
{
    public class ApplicationContext: DbContext
    {

        public ApplicationContext(DbContextOptions options):base(options)
        {
                
        }

        public DbSet<Application> Applications => Set<Application>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>().OwnsOne(a => a.Person).OwnsOne(p => p.Name);
            modelBuilder.Entity<Application>().OwnsOne(a => a.ReferralProcessInfo);
            modelBuilder.Entity<Application>().Property(c => c.DateInitiated).HasField("_initiated");
        }
    }
}
