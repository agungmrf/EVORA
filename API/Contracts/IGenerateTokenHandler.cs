using API.DTOs.Accounts;
using System.Security.Claims;

namespace API.Contracts;

public interface IGenerateTokenHandler
{
    // Untuk mengenerate token
    // Claim digunakan untuk menyimpan informasi user yang login
    string Generate(IEnumerable<Claim> claims);

    ClaimsDto ExtractClaimsFromJwt(string token);
}