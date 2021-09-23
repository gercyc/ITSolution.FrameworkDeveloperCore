using System;

namespace ITSolution.Framework.Blazor.Application.Interfaces.Services
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}