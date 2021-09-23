using ITSolution.Framework.Blazor.Application.Models.Chat;
using ITSolution.Framework.Blazor.Application.Responses.Identity;
using ITSolution.Framework.Blazor.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITSolution.Framework.Blazor.Application.Interfaces.Chat;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}