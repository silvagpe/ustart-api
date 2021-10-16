using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace UStart.API.TokenHelper
{
    public static class TokenExtensions
    {
        public static string GetToken(this HttpContext context)
        {
            return context.Request.Headers["Authorization"];
        }

        public static JwtSecurityToken GetJwtSecurityToken(this HttpContext context)
        {
            var token = context.GetToken();
            token = token.Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            return jwtSecurityToken;
        }

        public static string GetUsuarioId(this HttpContext context)
        {
            return GetClaimValueByKey(context, "usuarioid");
        }


        public static string GetClaimValueByKey(this HttpContext context, string key)
        {
            Claim claim = GetClaim(key, context);

            if (claim == null)
            {
                return string.Empty;
            }

            return claim.Value;
        }


        private static Claim GetClaim(string key, HttpContext context)
        {
            var jwtSecurityToken = GetJwtSecurityToken(context);


            if (jwtSecurityToken == null)
            {
                return null;
            }
            return jwtSecurityToken.Claims.Where(x => x.Type == key).FirstOrDefault();
        }


    }

}
