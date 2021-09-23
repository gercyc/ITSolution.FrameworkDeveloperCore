using System.Text.Json;
using ITSolution.Framework.Blazor.Application.Interfaces.Serialization.Options;

namespace ITSolution.Framework.Blazor.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}