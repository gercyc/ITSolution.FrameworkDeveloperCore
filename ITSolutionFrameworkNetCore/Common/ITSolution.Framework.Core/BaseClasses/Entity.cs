using System;
using ITSolution.Framework.Core.Common.BaseInterfaces;

namespace ITSolution.Framework.Core.Common.BaseClasses
{
    public class Entity<TPk> : IEntity<TPk>
    {
        public TPk Id { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }

    }
}
