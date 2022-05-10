using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITSolution.Framework.Core.Server.BaseClasses
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            //return context.GetHttpContext().User.Identity.IsAuthenticated;
            return true;
        }
    }
}
