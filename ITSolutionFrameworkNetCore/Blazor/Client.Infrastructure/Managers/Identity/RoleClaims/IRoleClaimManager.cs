using System.Collections.Generic;
using System.Threading.Tasks;
using ITSolution.Framework.Blazor.Application.Requests.Identity;
using ITSolution.Framework.Blazor.Application.Responses.Identity;
using ITSolution.Framework.Blazor.Shared.Wrapper;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}