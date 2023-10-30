using System.Security.Claims;
using API.DTOs.Accounts;

namespace API.Contracts;

public interface IGenerateTokenHandler
{
    // Untuk mengenerate token
    // Claim digunakan untuk menyimpan informasi user yang login
    string Generate(IEnumerable<Claim> claims);
    
    ClaimsDto ExtractClaimsFromJwt(string token);
}