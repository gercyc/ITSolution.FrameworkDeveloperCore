using System.Threading.Tasks;

namespace ITSolution.Framework.Blazor.Application.Interfaces.Repositories
{
    public interface IDocumentRepository
    {
        Task<bool> IsDocumentTypeUsed(int documentTypeId);

        Task<bool> IsDocumentExtendedAttributeUsed(int documentExtendedAttributeId);
    }
}