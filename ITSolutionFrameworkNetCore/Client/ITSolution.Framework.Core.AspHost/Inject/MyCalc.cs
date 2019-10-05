using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSolution.Framework.Core.AspHost.Inject
{
    public class MyCalc : IMyCalc
    {
        public decimal Sum(params decimal[] args)
        {
            decimal hResult = 0m;
            for (int i = 0; i < args.Length; i++)
            {
                hResult += args[i];
            }            
            return hResult;
        }
    }
}
