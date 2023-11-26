using Domain.Identity;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Services.Identity
{
    public class TokenIssuer : ITokenIssuer
    {
        private readonly IConfiguration configuration;

        public TokenIssuer(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public JwtSecurityToken Issue(Account user)
        {
            var secret = configuration["Jwt:Key"];
            var issuer = configuration["Jwt:Issuer"];
            var claims = GetTokenClaims(user);

            return new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)), SecurityAlgorithms.HmacSha256)
            );
        }

        public PlainToken IssuePlainToken(Account user, string refreshToken)
        {
            var token = Issue(user);
            return new PlainToken
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                ExpirationDate = token.ValidTo.ConvertToTimestamp()
            };
        }

        private static IEnumerable<Claim> GetTokenClaims(Account user)
        {
            yield return new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            yield return new Claim(ClaimTypes.Name, user.Username);
            yield return new Claim(ClaimTypes.NameIdentifier, "" + user.Id);
         
            foreach(var role in user.AccountRoles)
            {
                yield return new Claim(ClaimTypes.Role, role.Role.Name);
            }
         
            yield return new Claim(ITokenIssuer.Claims.Firstname, user.FirstName ?? "");
            yield return new Claim(ITokenIssuer.Claims.Lastname, user.LastName ?? "");
            yield return new Claim(ITokenIssuer.Claims.Id, "" + user.Id);
            yield return new Claim(ITokenIssuer.Claims.Username, user.Username ?? "");
        }
    }
}
