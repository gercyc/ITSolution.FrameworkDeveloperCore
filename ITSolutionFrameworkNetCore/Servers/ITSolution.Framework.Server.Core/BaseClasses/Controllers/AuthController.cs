using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ITSolution.Framework.Common.Abstractions.Identity;
using ITSolution.Framework.Common.Abstractions.Identity.DataAccess;
using ITSolution.Framework.Common.BaseClasses;
using ITSolution.Framework.Common.BaseClasses.Identity;
using ITSolution.Framework.Core.Server.BaseInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ITSolution.Framework.Core.Server.BaseClasses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ItsIdentityContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(ItsIdentityContext context, IOptions<JwtSettings> jwtSettings, ITokenService tokenService, UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "login api" };
        }


        /// <summary>
        /// Application login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("AuthLogin")]
        public async Task<AuthenticationResult> Login(AuthenticationRequest request)
        {
            return await _tokenService.Autenticate(request);
        }
        
        [AllowAnonymous]
        [HttpPost("LoginCookie")]
        public async Task<object> LoginCookie(ApplicationUser usuario)
        {
            bool credenciaisValidas = false;
            var userIdentity = await _userManager.FindByNameAsync(usuario.Email);
            await _signInManager.SignInAsync(userIdentity, true);

            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.Email))
            {
                if (userIdentity != null)
                {
                    // Efetua o login com base no Id do usuário e sua senha
                    await _signInManager.SignInAsync(userIdentity, false);
                    var c = await _signInManager.CreateUserPrincipalAsync(userIdentity);

                    credenciaisValidas = _signInManager.IsSignedIn(c);
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
        public IActionResult Register(AuthenticationRequest user)
        {
            var ok = _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = user.UserName,
                Email = user.UserName,
                EmailConfirmed = true,
            }, user.Password);

            if (ok.Result.Succeeded)
            {
                var us = _userManager.FindByNameAsync(user.UserName);
                _signInManager.SignInAsync(us.Result, true);
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(AuthenticationRequest user)
        {

            ApplicationUser userChange = _userManager.FindByNameAsync(user.UserName).Result;

            var ok = _userManager.ChangePasswordAsync(userChange, user.Password, user.Password);

            if (ok.Result.Succeeded)
                return Ok();
            else
                return BadRequest(ok.Result.Errors);
        }

        [HttpPut("updateuser/{id}")]
        public async Task<IActionResult> UpdateUser(string id, AuthenticationRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser currentUser = await _userManager.FindByIdAsync(id.ToString());
            currentUser.Email = user.UserName;

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
