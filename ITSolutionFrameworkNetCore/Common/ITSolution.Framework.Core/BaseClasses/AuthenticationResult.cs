using System;

namespace ITSolution.Framework.Core.Common.BaseClasses;

public class AuthenticationResult
{
    public string Token { get; set; }
    public DateTime Created { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string UserName { get; set; }
    public bool IsAuthenticated { get; set; }
}