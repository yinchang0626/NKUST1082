using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<YC.Models.Station> Stations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=aspnet-WebApplication-EC63BA8C-EBF0-4A89-B614-9EB716D5226A;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
