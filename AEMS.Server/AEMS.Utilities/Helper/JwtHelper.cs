using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;


namespace AEMS.Utilities.Helper
{
    public class JwtHelper
    {
        public static UserSession ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSercurity = tokenHandler.ReadJwtToken(token);

            var identityClaim = jwtSercurity.Claims.ToList();

            identityClaim.RemoveAll(x => x.Type == "amr");

            var dictionaryClaim = identityClaim.ToDictionary(x => x.Type, x => x.Value);

            return new UserSession()
            {
                Id = Guid.Parse(dictionaryClaim["oid"]),
                Name = dictionaryClaim["name"],
                Email = dictionaryClaim["email"],
                Country = dictionaryClaim["ctry"],
                Token = token
            };
        }

    }
}
