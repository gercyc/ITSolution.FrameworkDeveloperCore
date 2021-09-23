
using ITSolution.Framework.Blazor.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace ITSolution.Framework.Blazor.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}