using City.Api.Model;

namespace City.Api
{
    public class CitiesDtoStore
    {
        public List<CitiesDto> Cities { get; set; }
        //public static CitiesDtoStore Current { get; } = new CitiesDtoStore();
        public CitiesDtoStore()
        {
            Cities = new List<CitiesDto>()
            {
                new CitiesDto()
                {
                    Id = 1,
                    Name = "Lagos",
                    Description = "Most populated and commerial city in Nigeria",

                    PointsOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id= 1,
                            Name = "Fala Shrine",
                            Description = "An amusement shrine in honor of legend Fala Kuti"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Eko Hotel",
                            Description = "Famous hotel in the island region of Lagos"
                        }
                        
                    }

                    
                },

                new CitiesDto()
                {
                    Id = 2,
                    Name = "Benin",
                    Description = "A traditional beautiful city",

                    PointsOfInterests= new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Oba Place",
                            Description = "The famous benin king palace"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "University of Benin",
                            Description = "The oldest university in the south south region of Nigeria"
                        }
                    }
                },

                new CitiesDto()
                {
                    Id = 3,
                    Name = "Port-Harcourt",
                    Description = "Rich oil city",
                    PointsOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "University of Port Harcourt",
                            Description = "The biggest university in Nigeria, located at Choba Community"
                        },
                        new PointOfInterestDto()
                        {
                             Id = 6,
                            Name = "Port-Harcout city park",
                            Description = "The biggest amusement part in the city, located at Roumokoro"
                        }

                    }
                },

            };
        }



        //public static List<CitiesDto> cityList = new List<CitiesDto>
        //{
        //   new CitiesDto{ Id = 1, Name = "Lagos", Description = "Most populated and commerial city in Nigeria" ,},
        //   new CitiesDto{ Id = 2, Name = "Port Harcourt", Description = "ekwmfkwm"},


        //};
    }
}
