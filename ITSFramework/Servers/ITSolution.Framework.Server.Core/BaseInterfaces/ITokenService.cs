using System.Threading.Tasks;
using ITSolution.Framework.Common.BaseClasses;

namespace ITSolution.Framework.Core.Server.BaseInterfaces;

public interface ITokenService
{
    Task<AuthenticationResult> Autenticate(AuthenticationRequest request);
}