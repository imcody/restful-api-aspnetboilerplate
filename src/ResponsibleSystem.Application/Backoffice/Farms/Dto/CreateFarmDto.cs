namespace ResponsibleSystem.Backoffice.Farms.Dto
{
    public class CreateFarmDto
    {
        public string Name { get; set; }
        public string OrganizationNumber { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
