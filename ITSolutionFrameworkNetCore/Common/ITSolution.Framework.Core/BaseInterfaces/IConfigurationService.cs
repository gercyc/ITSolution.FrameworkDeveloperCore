namespace ITSolution.Framework.Core.Common.BaseInterfaces
{
    public interface IConfigurationService
    {
        string GetConnectionString(string key);
    }
}
