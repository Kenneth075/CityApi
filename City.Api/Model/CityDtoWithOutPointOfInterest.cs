namespace City.Api.Model
{
    public class CityDtoWithOutPointOfInterest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
