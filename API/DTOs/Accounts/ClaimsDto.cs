namespace API.DTOs.Accounts;

public class ClaimsDto
{
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Role { get; set; }
}