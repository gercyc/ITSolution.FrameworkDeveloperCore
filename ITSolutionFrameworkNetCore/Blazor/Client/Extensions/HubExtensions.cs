using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using ITSolution.Framework.Blazor.Shared.Constants.Application;

namespace ITSolution.Framework.Blazor.Client.Extensions
{
    public static class HubExtensions
    {
        public static HubConnection TryInitialize(this HubConnection hubConnection, NavigationManager navigationManager)
        {
            if (hubConnection == null)
            {
                hubConnection = new HubConnectionBuilder()
                                  .WithUrl(navigationManager.ToAbsoluteUri(ApplicationConstants.SignalR.HubUrl))
                                  .Build();
            }
            return hubConnection;
        }
    }
}