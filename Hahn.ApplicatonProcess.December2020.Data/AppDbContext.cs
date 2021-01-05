using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        //    builder.Entity<Applicant>().Property(x => x.DateCreated).ValueGeneratedOnAdd().HasValueGenerator(new ValueGenerator(;

        //    builder.Entity<Applicant>().Property(x => x.FamilyName).HasDefaultValue()
        //    builder.Entity<Applicant>().HasIndex(x => x.EmailAddress).IsUnique();
        }
    }

}
