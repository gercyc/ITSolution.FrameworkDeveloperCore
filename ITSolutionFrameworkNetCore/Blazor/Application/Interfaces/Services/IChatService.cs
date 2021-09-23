using ITSolution.Framework.Blazor.Application.Responses.Identity;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITSolution.Framework.Blazor.Application.Interfaces.Chat;
using ITSolution.Framework.Blazor.Application.Models.Chat;

namespace ITSolution.Framework.Blazor.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}