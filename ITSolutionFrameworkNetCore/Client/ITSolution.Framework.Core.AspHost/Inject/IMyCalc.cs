using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSolution.Framework.Core.AspHost.Inject
{
    public interface IMyCalc
    {
        decimal Sum(params decimal[]  args);
    }
}
