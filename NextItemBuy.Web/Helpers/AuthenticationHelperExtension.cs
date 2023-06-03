using Microsoft.IdentityModel.Tokens;
using NextItemBuy.Services.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace NextItemBuy.Web.Helpers
{
    public static class AuthenticationHelperExtension
    {
        private static string Secret = "aZ5cG3e2tLlWcrnK2TVDJuMRfSFlF27iqDiBv4xKW74=";

        public static bool ValidateToken(this HttpContext context)
        {

            var token = context.Request.Headers["Authorization"];
            if (token == null)
            {
                return false;
            }

            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var simplePrinciple = GetPrincipal(token);
            context.User = Thread.CurrentPrincipal = simplePrinciple;

            if (simplePrinciple == null)
                return false;

            if (!simplePrinciple.Identity.IsAuthenticated)
                return false;

            return true;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                {
                    return null;
                }

                byte[] key = Convert.FromBase64String(Secret);

                var parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public static string GenerateToken(this HttpContext context, UserModel user)
        {
            byte[] key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserId", user.Id.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };


            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(descriptor);
            var tkn = handler.WriteToken(token);

            context.Response.Headers.Add("token", tkn);

            return tkn;
        }
    }
}