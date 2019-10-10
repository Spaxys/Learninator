using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Learninator.Models;

namespace Learninator.Database
{
    public class LearninatorContext : DbContext
    {
            public LearninatorContext(DbContextOptions<LearninatorContext> options)
                : base(options)
            {
            }
        public DbSet<Link> Links { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Link>().ToTable("Link");
                modelBuilder.Entity<Tag>().ToTable("Tag");
        }
    }
}
