using System.Linq;
using ITSolution.Framework.Blazor.Shared.Constants.Localization;
using ITSolution.Framework.Blazor.Shared.Settings;

namespace ITSolution.Framework.Blazor.Client.Infrastructure.Settings
{
    public record ClientPreference : IPreference
    {
        public bool IsDarkMode { get; set; }
        public bool IsRTL { get; set; }
        public bool IsDrawerOpen { get; set; }
        public string PrimaryColor { get; set; }
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";
    }
}