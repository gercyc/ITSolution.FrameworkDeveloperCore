using System.Text.Json;

namespace ITSolution.Framework.Blazor.Application.Interfaces.Serialization.Options
{
    public interface IJsonSerializerOptions
    {
        /// <summary>
        /// Options for <see cref="System.Text.Json"/>.
        /// </summary>
        public JsonSerializerOptions JsonSerializerOptions { get; }
    }
}