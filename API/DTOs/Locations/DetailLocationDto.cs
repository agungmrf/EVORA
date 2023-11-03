namespace API.DTOs.Locations
{
    public class DetailLocationDto
    {
        public Guid Guid { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        // public Guid GuidCity { get; set; }
        public string City { get; set; }
        // public Guid GuidProvince { get; set; }
        public string Province { get; set; }

    }
}
