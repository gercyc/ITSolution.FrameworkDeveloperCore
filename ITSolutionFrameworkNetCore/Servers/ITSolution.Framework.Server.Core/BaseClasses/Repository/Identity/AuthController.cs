using ITSolution.Framework.Core.BaseClasses.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITSIdentityContext _context;

        public AuthController(ITSIdentityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "login api" };
        }

        /// <summary>
        /// Application login
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="signingConfigurations"></param>
        /// <param name="tokenConfigurations"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public object Login(
            [FromBody]ApplicationUser usuario,
            [FromServices]UserManager<ApplicationUser> userManager,
            [FromServices]SignInManager<ApplicationUser> signInManager,
            [FromServices]SignInConfiguration signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {
            bool credenciaisValidas = false;
            // Verifica a existência do usuário nas tabelas do
            // ASP.NET Core Identity
            var userIdentity = userManager
                .FindByNameAsync(usuario.Email).Result;
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.Email))
            {
                if (userIdentity != null)
                {
                    // Efetua o login com base no Id do usuário e sua senha
                    var signInResult = signInManager.CheckPasswordSignInAsync(userIdentity, usuario.Password, false);
                    if (signInResult.Result.Succeeded)
                    {
                        var c = signInManager.CreateUserPrincipalAsync(userIdentity);
                        credenciaisValidas = signInManager.IsSignedIn(c.Result);
                    }
                }
            }

            if (credenciaisValidas)
            {

                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(userIdentity.UserName, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, userIdentity.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userIdentity.UserName)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    userId = userIdentity.Id,
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

        [AllowAnonymous]
        [HttpPost("LoginCookie")]
        public async Task<object> LoginCookie(
         [FromBody]ApplicationUser usuario,
         [FromServices]UserManager<ApplicationUser> userManager,
         [FromServices]SignInManager<ApplicationUser> signInManager)
        {
            bool credenciaisValidas = false;
            var userIdentity = await userManager.FindByNameAsync(usuario.Email);
            await signInManager.SignInAsync(userIdentity, true);

            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.Email))
            {
                if (userIdentity != null)
                {
                    // Efetua o login com base no Id do usuário e sua senha
                    await signInManager.SignInAsync(userIdentity, false);
                    var c = await signInManager.CreateUserPrincipalAsync(userIdentity);

                    credenciaisValidas = signInManager.IsSignedIn(c);
                }
            }

            if (credenciaisValidas)
            {
                return new
                {
                    authenticated = true,
                    userId = userIdentity.Id,
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

        [HttpPost("Register")]
        public IActionResult Register([FromServices]UserManager<ApplicationUser> userManager,
                    [FromServices]SignInManager<ApplicationUser> signInManager,
                    [FromBody] ApplicationUser user)
        {


            var ok = userManager.CreateAsync(new ApplicationUser()
            {
                UserName = user.Email,
                Email = user.Email,
                EmailConfirmed = true,
            }, user.Password);

            if (ok.Result.Succeeded)
            {
                var us = userManager.FindByNameAsync(user.Email);
                signInManager.SignInAsync(us.Result, true);
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromServices]UserManager<ApplicationUser> userManager,
            [FromServices]SignInManager<ApplicationUser> signInManager,
            [FromBody] ApplicationUser user)
        {

            ApplicationUser userChange = userManager.FindByNameAsync(user.Email).Result;

            var ok = userManager.ChangePasswordAsync(userChange, user.Password, user.NewPassword);

            if (ok.Result.Succeeded)
                return Ok();
            else
                return BadRequest(ok.Result.Errors);
        }

        [HttpPut("updateuser/{id}")]
        public async Task<IActionResult> UpdateUser([FromServices]UserManager<ApplicationUser> userManager, [FromRoute] int id, [FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser currentUser = await userManager.FindByIdAsync(id.ToString());
            currentUser.Email = user.Email;
            currentUser.PhoneNumber = user.PhoneNumber;
            currentUser.Ancord = user.Ancord;
            currentUser.Endereco = user.Endereco;

            _context.Entry(currentUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
