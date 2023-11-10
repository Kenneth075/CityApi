namespace City.Api.Model
{
    public class CitiesDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfPointOfInterest        //This contains the number of interest in the city
        {
            get
            {
                return PointsOfInterests.Count; 
            }

        }

        //Initializing the collection to an empty collection instead of leaving it as null to avoid null reference issue.
        public ICollection<PointOfInterestDto> PointsOfInterests { get; set; } = new List<PointOfInterestDto>();



    }
}
