using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Portfolio.API.DAO;
using Portfolio.API.Model.Security;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthTokenController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public object Post(
            [FromBody] User usuario,
            [FromServices] UsersDAO usersDAO,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {
            bool credenciaisValidas = false;
            User usuarioBase = null;
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.UserID))
            {
                try
                {
                    usuarioBase = usersDAO.FindByUser(usuario.UserID);
                }
                catch (Exception ex)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Ao consultar UserID - " + ex.Message
                    };
                }

                if (usuarioBase != null)
                {
                    credenciaisValidas = (usuarioBase != null &&
                    usuario.UserID == usuarioBase.UserID &&
                    usuario.AccessKey == usuarioBase.AccessKey);
                }
                else
                {
                    return new
                    {
                        authenticated = false,
                        message = "UserID Não encontrado na base!"
                    };
                };
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.UserID, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UserID)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = DateTime.Now.AddMinutes(5);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    Expires = dataExpiracao,
                    NotBefore = dataCriacao
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }
        private bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
    }
}
