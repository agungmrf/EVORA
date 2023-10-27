namespace API.DTOs.Locations
{
    public class DetailLocationDto
    {
        public Guid Guid { get; set; }
        public string Invoice { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

    }
}
