using API.Utilities.Enums;

namespace API.DTOs.TransactionEvents
{
    public class TransactionDetailDto
    {   
        // customer
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderLevel? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? AccountGuid { get; set; }
        //Package
        public string? Package { get; set; }
        public int? Capacity { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        //Location
        public string? Street { get; set; }
        public string? SubDistrict { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        // Transaction
        public Guid? Guid { get; set; }
        public string Invoice { get; set; }
        public DateTime EventDate { get; set; }
        public StatusTransaction Status { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid CustomerGuid { get; set; }
        public Guid PackageGuid { get; set; }
        public Guid LocationGuid { get; set; }
    }
}
