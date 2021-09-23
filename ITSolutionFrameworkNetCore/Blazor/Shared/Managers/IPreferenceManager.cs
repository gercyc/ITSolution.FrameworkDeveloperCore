using ITSolution.Framework.Blazor.Shared.Settings;
using System.Threading.Tasks;
using ITSolution.Framework.Blazor.Shared.Wrapper;

namespace ITSolution.Framework.Blazor.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}