using ITSolution.Framework.Blazor.Application.Interfaces.Services;
using System;

namespace ITSolution.Framework.Blazor.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}