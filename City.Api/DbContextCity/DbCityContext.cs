using City.Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace City.Api.DbContextCity
{
    public class DbCityContext : DbContext
    {
        public DbSet<Citie> Cities { get; set; } = null!;
        public DbSet<PointsOfInterest> PointsOfInterests { get; set; } = null!;

        public DbCityContext(DbContextOptions<DbCityContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citie>().HasData(
                new Citie("Lagos")
                {
                    Id = 1,
                    Description = "The most populated city in Nigeria"
                },
                new Citie("Port-Harcourt")
                {
                    Id = 2,
                    Description = "Oil rich city"
                },
                new Citie("Benin-City")
                {
                    Id = 3,
                    Description = "Nigeria most ancient kingdow"
                });

            modelBuilder.Entity<PointsOfInterest>().HasData(
                new PointsOfInterest("Eko Hotel")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "5 stars hotel at Lagos VI"
                },
                new PointsOfInterest("LandMark Beach")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "A very beautiful at the Lagos Island"
                },
                new PointsOfInterest("University of Port-Harcourt")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "The prestigious university of Port-Harcourt located Choba"
                },
                new PointsOfInterest("Port-Harcourt city Park")
                {
                    Id = 4,
                    CityId = 2,
                    Description = "A beautiful amusement park"
                },
                new PointsOfInterest("Oba Palace")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "The beautiful palace of His Royal Highest of Benin Kingdom"
                },
                new PointsOfInterest("Decagon Tech Park")
                {
                    Id = 6,
                    CityId = 3,
                    Description = "A learning institute where young Nigerias are training to become elite software engineers"
                });

            base.OnModelCreating(modelBuilder);
                
                
        }


        //Alternative configuration
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionString");
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}
