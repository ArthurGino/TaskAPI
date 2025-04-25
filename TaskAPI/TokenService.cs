using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskAPI.Model;

namespace TaskAPI
{
    public class TokenService
    {
        public static string CreateToken(Register usuario)
        {

            List<Claim> claims = new List<Claim>()
            {
                new Claim("Id", usuario.Id.ToString()),
                new Claim("Email", usuario.Email.ToString())
            };

           var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("hhdfjyrkluço8d.96tutrtfghcygjhfjfjuyjri57zrykrt"));

           var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

           var token = new JwtSecurityToken

            (claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}