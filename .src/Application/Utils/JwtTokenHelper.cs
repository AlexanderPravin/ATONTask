namespace Application.Utils;

public class JwtTokenHelper(IConfiguration configuration)
{
    public string CreateToken(string role, string login)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, login),
            new (ClaimTypes.Role, role)
        };

        var claimsIdentities = new ClaimsIdentity(claims);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = configuration["JWT:Issuer"],
            Subject = claimsIdentities,
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(descriptor);

        return tokenHandler.WriteToken(token);
    }
}