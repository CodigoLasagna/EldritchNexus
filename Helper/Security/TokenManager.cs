using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace Helper.Security;

public class TokenManager
{
    private readonly string _jwtSecret = "LauraLaQueVendeMangosEnElMercado";

    public TokenManager(string jwtSecret)
    {
        _jwtSecret = jwtSecret;
    }

    public TokenManager(){}

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
    }

    public bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
    }

    public string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim("nickname", user.Nickname),
            new Claim("roleId", user.UserRoleId.ToString()),
            new Claim("profileimageurl", user.ProfileImageUrl),
        };
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public int ValidateToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return -1;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSecret);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userIdClaim = jwtToken.Claims.FirstOrDefault( x=> x.Type == "id" );
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
        }
        catch
        {
            return -1;
        }

        return -1;
    }
    public string RenewJwtToken(string token, User updatedUser)
    {
        int userId = ValidateToken(token);
        if (userId == -1 || userId != updatedUser.Id)
        {
            throw new SecurityTokenException("Token inválido o no coincide con el usuario");
        }
    
        return GenerateJwtToken(updatedUser);
    }
}