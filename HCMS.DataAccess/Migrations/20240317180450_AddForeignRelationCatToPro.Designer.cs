﻿// <auto-generated />
using HCMS.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HCMS.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240317180450_AddForeignRelationCatToPro")]
    partial class AddForeignRelationCatToPro
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HCMS.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DisplayCategory")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayCategory = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            DisplayCategory = 1,
                            Name = "Sci"
                        },
                        new
                        {
                            Id = 3,
                            DisplayCategory = 1,
                            Name = "His"
                        });
                });

            modelBuilder.Entity("HCMS.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListPrice")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Price100")
                        .HasColumnType("int");

                    b.Property<int>("Price50")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Billy Spark",
                            CategoryId = 1,
                            Description = "Present Vitae sodales libero.",
                            ISBN = "SWD9999001",
                            ListPrice = 99,
                            Price = 90,
                            Price100 = 80,
                            Price50 = 85,
                            Title = "Fortune of Time"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Billy Spark",
                            CategoryId = 2,
                            Description = "Present Vitae sodales libero.",
                            ISBN = "SWD9999001",
                            ListPrice = 99,
                            Price = 90,
                            Price100 = 80,
                            Price50 = 85,
                            Title = "Fortune of Times"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Billy Spark",
                            CategoryId = 3,
                            Description = "Present Vitae sodales libero.",
                            ISBN = "SWD9999001",
                            ListPrice = 99,
                            Price = 95,
                            Price100 = 80,
                            Price50 = 85,
                            Title = "Fortune of Times"
                        });
                });

            modelBuilder.Entity("HCMS.Models.Product", b =>
                {
                    b.HasOne("HCMS.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
