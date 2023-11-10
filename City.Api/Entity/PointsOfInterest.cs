using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace City.Api.Entity
{
    public class PointsOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public Citie? City { get; set; }
        public int CityId { get; set; }

        public PointsOfInterest(string name)
        {
            Name = name;
        }
    }
}
