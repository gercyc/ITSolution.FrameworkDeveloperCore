using System.Collections.Generic;
using System.Threading.Tasks;
using ITSolution.Framework.Blazor.Application.Interfaces.Common;
using ITSolution.Framework.Blazor.Application.Requests.Identity;
using ITSolution.Framework.Blazor.Application.Responses.Identity;
using ITSolution.Framework.Blazor.Shared.Wrapper;

namespace ITSolution.Framework.Blazor.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}