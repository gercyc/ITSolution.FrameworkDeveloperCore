﻿using System.Linq;
using ITSolution.Framework.Blazor.Shared.Constants.Localization;
using ITSolution.Framework.Blazor.Shared.Settings;

namespace ITSolution.Framework.Blazor.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}