using Domain.Identity;
using Models.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Services.Identity
{
    public interface ITokenIssuer
    {

        public static class Claims
        {
            public static readonly string Id = "Id";
            public static readonly string Username = "Username";
            public static readonly string Firstname = "Firstname";
            public static readonly string Lastname = "Lastname";
        }

        JwtSecurityToken Issue(Account user);
        PlainToken IssuePlainToken(Account user, string refreshToken);
    }
}
