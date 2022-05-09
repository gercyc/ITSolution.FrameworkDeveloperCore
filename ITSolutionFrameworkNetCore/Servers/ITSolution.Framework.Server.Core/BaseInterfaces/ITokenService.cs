using System.Threading.Tasks;
using ITSolution.Framework.Core.Common.BaseClasses;

namespace ITSolution.Framework.Core.Server.BaseInterfaces;

public interface ITokenService
{
    Task<AuthenticationResult> Autenticate(AuthenticationRequest request);
}