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
        public DbSet<LinkTag> LinkTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            modelBuilder.Entity<LinkTag>()
                .HasKey(bc => new { bc.LinkId, bc.TagId });
            modelBuilder.Entity<LinkTag>()
                .HasOne(lt => lt.Link)
                .WithMany(l => l.LinkTags)
                .HasForeignKey(lt => lt.LinkId);
            modelBuilder.Entity<LinkTag>()
                .HasOne(lt => lt.Tag)
                .WithMany(l => l.LinkTags)
                .HasForeignKey(lt => lt.TagId);

                modelBuilder.Entity<Link>().ToTable("Link");
                modelBuilder.Entity<Tag>().ToTable("Tag");
                modelBuilder.Entity<LinkTag>().ToTable("LinkTag");
        }
    }
}
