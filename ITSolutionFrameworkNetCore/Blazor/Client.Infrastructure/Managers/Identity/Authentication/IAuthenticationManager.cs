using ITSolution.Framework.Blazor.Application.Requests.Identity;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Managers.Identity.Authentication
{
    public interface IAuthenticationManager : IManager
    {
        Task<IResult> Login(TokenRequest model);

        Task<IResult> Logout();

        Task<string> RefreshToken();

        Task<string> TryRefreshToken();

        Task<string> TryForceRefreshToken();

        Task<ClaimsPrincipal> CurrentUser();
    }
}