using System.Collections.Generic;

namespace ITSolution.Framework.Blazor.Application.Responses.Identity
{
    public class GetAllUsersResponse
    {
        public IEnumerable<UserResponse> Users { get; set; }
    }
}