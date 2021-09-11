using ITSolution.Framework.Core.BaseClasses.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ITSolution.Framework.Core.Server.BaseClasses.Repository.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITSIdentityContext _context;


        public AuthController()
        {
            ItsDbContextOptions opt = new ItsDbContextOptions();
            _context = new ITSIdentityContext(opt);
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "login api" };
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<object> Login(
            [FromBody]ApplicationUser usuario,
            [FromServices]UserManager<ApplicationUser> userManager,
            [FromServices]SignInManager<ApplicationUser> signInManager)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(usuario.UserName, usuario.Password, true, false);
            
            if (result.Succeeded)
            {
                return new
                {
                    authenticated = true,
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
        public object LoginCookie(
         [FromBody]ApplicationUser usuario,
         [FromServices]UserManager<ApplicationUser> userManager,
         [FromServices]SignInManager<ApplicationUser> signInManager)
        {
            bool credenciaisValidas = false;
            var userIdentity = userManager
                .FindByNameAsync(usuario.Email).Result;
            signInManager.SignInAsync(userIdentity, true);
            
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.Email))
            {
                if (userIdentity != null)
                {
                    // Efetua o login com base no Id do usuário e sua senha
                    signInManager.SignInAsync(userIdentity, false);
                    var c = signInManager.CreateUserPrincipalAsync(userIdentity).Result;
                    
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
        public async Task<IActionResult> UpdateUser([FromServices]UserManager<ApplicationUser> userManager, [FromRoute] string id, [FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser currentUser = await userManager.FindByIdAsync(id);
            currentUser.Email = user.Email;            
            currentUser.PhoneNumber = user.PhoneNumber;

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
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
