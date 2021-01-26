using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    [EnableCors] 
    [Route("api/[controller]")]
    public class AuthTokenController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        
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
                log.Info("Requisição de autenticação - UserID: " + usuario.UserID);
                try
                {
                    log.Info("Solicitando credenciais da base de dados - UserID: " + usuario.UserID);
                    usuarioBase = usersDAO.FindByUser(usuario.UserID);
                }
                catch (Exception ex)
                {
                    log.Error("Erro ao consultar o banco UserID: " + usuario.UserID + " - Error: " + ex.Message);
                    return new
                    {
                        authenticated = false,
                        message = "Erro ao consultar UserID - " + ex.Message
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
                    log.Warn("Usuário não encontado na base - UserID: " + usuario.UserID);
                    Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return new
                    {
                        authenticated = false,
                        message = "UserID Não encontrado na base!"
                    };
                };
            }

            if (credenciaisValidas)
            {
                log.Info("Credenciais válidas - UserID: " + usuario.UserID);

                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.UserID, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UserID)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = DateTime.Now.AddMinutes(5);

                log.Info("Realizando a criação do token - UserID: " + usuario.UserID);

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

                log.Info("Token criado com sucesso - UserID: " + usuario.UserID);

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
                log.Warn("Falha na autenticação do token - UserID: " + usuario.UserID);
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }
    }
}
