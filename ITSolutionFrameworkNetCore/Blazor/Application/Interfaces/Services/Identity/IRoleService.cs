using ITSolution.Framework.Blazor.Application.Interfaces.Common;
using ITSolution.Framework.Blazor.Application.Requests.Identity;
using ITSolution.Framework.Blazor.Application.Responses.Identity;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Application.Interfaces.Services.Identity
{
    public interface IRoleService : IService
    {
        Task<Result<List<RoleResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleResponse>> GetByIdAsync(string id);

        Task<Result<string>> SaveAsync(RoleRequest request);

        Task<Result<string>> DeleteAsync(string id);

        Task<Result<PermissionResponse>> GetAllPermissionsAsync(string roleId);

        Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}