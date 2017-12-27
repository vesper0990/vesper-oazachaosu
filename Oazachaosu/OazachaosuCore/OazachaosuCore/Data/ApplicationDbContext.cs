﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace OazachaosuCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<Models.ApplicationUser>, IDatabaseContext
    {

        private static bool test = false;

        public DbSet<Group> Groups { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Result> Results { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            test = true;
        }

        public ApplicationDbContext() : base()
        {
        }

        public Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(default(CancellationToken));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (test)
            {
                return;
            }
            optionsBuilder.UseMySql(@"Server=localhost;database=test;uid=root;pwd=Akuku123;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Group>()
                .HasMany(g => g.Words)
                .WithOne(w => w.Group);

            builder.Entity<Group>()
                .HasMany(g => g.Results)
                .WithOne(r => r.Group);
        }

    }
}
