using ITSolution.Framework.Core.Common.BaseClasses;
using ITSolution.Framework.Core.Common.BaseClasses.Identity;
using ITSolution.Framework.Core.Server.BaseClasses.Configurators;
using ITSolution.Framework.Core.Server.BaseInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ITSolution.Framework.Core.Server.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public TokenService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();//jwtSettings.Value;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    public async Task<AuthenticationResult> Autenticate(AuthenticationRequest request)
    {
        if (request.UserName == null || request.Password == null)
            throw new BadHttpRequestException("UserName and Password are required!");

        var userIdentity = await _userManager.FindByNameAsync(request.UserName);
        if (userIdentity == null)
            throw new BadHttpRequestException("UserName not found!");

        var signInResult = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
        AuthenticationResult result = new AuthenticationResult();

        if (signInResult.Succeeded)
        {
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(userIdentity);
            result.IsAuthenticated = _signInManager.IsSignedIn(claimsPrincipal);
            result.UserName = userIdentity.UserName;
            result.Created = DateTime.UtcNow;
            result.ExpirationDate = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes);
            result.Token = GenerateJwtToken(userIdentity);
        }

        return result;

    }
    private string GenerateJwtToken(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserName) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
}