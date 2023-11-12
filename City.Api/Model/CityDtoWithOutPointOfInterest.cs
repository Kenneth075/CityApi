namespace City.Api.Model
{
    /// <summary>
    /// Dto for a city without point of interest
    /// </summary>
    public class CityDtoWithOutPointOfInterest
    {
        /// <summary>
        /// id of the city
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the city
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Description of the city
        /// </summary>
        public string? Description { get; set; }
    }
}
