using ITSolution.Framework.Blazor.Application.Responses.Identity;
using System.Collections.Generic;

namespace ITSolution.Framework.Blazor.Application.Requests.Identity
{
    public class UpdateUserRolesRequest
    {
        public string UserId { get; set; }
        public IList<UserRoleModel> UserRoles { get; set; }
    }
}