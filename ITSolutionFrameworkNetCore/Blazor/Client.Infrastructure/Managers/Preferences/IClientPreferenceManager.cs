using ITSolution.Framework.Blazor.Shared.Managers;
using MudBlazor;
using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Managers.Preferences
{
    public interface IClientPreferenceManager : IPreferenceManager
    {
        Task<MudTheme> GetCurrentThemeAsync();

        Task<bool> ToggleDarkModeAsync();
    }
}