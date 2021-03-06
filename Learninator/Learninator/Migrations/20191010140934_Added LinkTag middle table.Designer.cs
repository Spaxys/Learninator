﻿// <auto-generated />
using Learninator.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Learninator.Migrations
{
    [DbContext(typeof(LearninatorContext))]
    [Migration("20191010140934_Added LinkTag middle table")]
    partial class AddedLinkTagmiddletable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Learninator.Models.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Link");
                });

            modelBuilder.Entity("Learninator.Models.LinkTag", b =>
                {
                    b.Property<int>("LinkId");

                    b.Property<int>("TagId");

                    b.HasKey("LinkId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("LinkTag");
                });

            modelBuilder.Entity("Learninator.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Learninator.Models.LinkTag", b =>
                {
                    b.HasOne("Learninator.Models.Link", "Link")
                        .WithMany("LinkTags")
                        .HasForeignKey("LinkId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Learninator.Models.Tag", "Tag")
                        .WithMany("LinkTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
