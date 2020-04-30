using CoreTestApp.Model;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTestApp.Services
{
    public class TestAppDbContext : IdentityDbContext<Worker>
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Company> Companies { get; set; }

        

        public TestAppDbContext( DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Company>()
               .Property(a => a.RowVersion)
               .IsConcurrencyToken()
               .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Worker>()
                .Ignore(a => a.EmailConfirmed)
                .Ignore(a => a.LockoutEnd)
                .Ignore(a => a.LockoutEnabled)
                .Ignore(a => a.Password)
                .Ignore(a => a.PhoneNumberConfirmed)
                .Ignore(a => a.TwoFactorEnabled)
                .Ignore(a => a.AccessFailedCount);


            base.OnModelCreating(modelBuilder);
        }
       
    }
}
