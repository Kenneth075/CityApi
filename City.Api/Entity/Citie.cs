using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace City.Api.Entity
{
    public class Citie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public Citie(string name)
        {
            Name = name;
        }

        public ICollection<PointsOfInterest> PointsOfInterest { get; set; } = new List<PointsOfInterest>();
    }
}
