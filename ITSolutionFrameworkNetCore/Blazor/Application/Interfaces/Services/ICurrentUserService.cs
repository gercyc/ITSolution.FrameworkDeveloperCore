using ITSolution.Framework.Blazor.Application.Interfaces.Common;

namespace ITSolution.Framework.Blazor.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}