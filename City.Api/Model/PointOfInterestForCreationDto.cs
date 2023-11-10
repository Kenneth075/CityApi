using System.ComponentModel.DataAnnotations;

namespace City.Api.Model
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage ="You should provide a name value")]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
