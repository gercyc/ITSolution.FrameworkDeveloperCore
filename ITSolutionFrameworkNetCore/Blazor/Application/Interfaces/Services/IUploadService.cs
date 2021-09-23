using ITSolution.Framework.Blazor.Application.Requests;

namespace ITSolution.Framework.Blazor.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}