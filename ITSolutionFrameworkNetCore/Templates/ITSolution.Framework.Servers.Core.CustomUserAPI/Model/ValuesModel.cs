using System;
using System.Collections.Generic;
using System.Text;

namespace ITSolution.Framework.Core.CustomUserAPI.Model
{
    public class ValuesModel
    {
        public ValuesModel()
        {
            Value_1 = "This is value 1";
            Value_2 = "This is value 2";
        }
        public string Value_1 { get; set; }
        public string Value_2 { get; set; }
    }
}
