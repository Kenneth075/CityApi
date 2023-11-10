﻿// <auto-generated />
using City.Api.DbContextCity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace City.Api.Migrations
{
    [DbContext(typeof(DbCityContext))]
    partial class DbCityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.24");

            modelBuilder.Entity("City.Api.Entity.Citie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The most populated city in Nigeria",
                            Name = "Lagos"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Oil rich city",
                            Name = "Port-Harcourt"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Nigeria most ancient kingdow",
                            Name = "Benin-City"
                        });
                });

            modelBuilder.Entity("City.Api.Entity.PointsOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PointsOfInterests");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Description = "5 stars hotel at Lagos VI",
                            Name = "Eko Hotel"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 1,
                            Description = "A very beautiful at the Lagos Island",
                            Name = "LandMark Beach"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 2,
                            Description = "The prestigious university of Port-Harcourt located Choba",
                            Name = "University of Port-Harcourt"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 2,
                            Description = "A beautiful amusement park",
                            Name = "Port-Harcourt city Park"
                        },
                        new
                        {
                            Id = 5,
                            CityId = 3,
                            Description = "The beautiful palace of His Royal Highest of Benin Kingdom",
                            Name = "Oba Palace"
                        },
                        new
                        {
                            Id = 6,
                            CityId = 3,
                            Description = "A learning institute where young Nigerias are training to become elite software engineers",
                            Name = "Decagon Tech Park"
                        });
                });

            modelBuilder.Entity("City.Api.Entity.PointsOfInterest", b =>
                {
                    b.HasOne("City.Api.Entity.Citie", "City")
                        .WithMany("Points")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("City.Api.Entity.Citie", b =>
                {
                    b.Navigation("Points");
                });
#pragma warning restore 612, 618
        }
    }
}
