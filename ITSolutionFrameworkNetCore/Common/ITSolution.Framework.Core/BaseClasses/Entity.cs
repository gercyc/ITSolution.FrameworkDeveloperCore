using ITSolution.Framework.Core.BaseClasses.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITSolution.Framework.Core.BaseClasses
{
    public class Entity<Tpk> where Tpk : IEquatable<Tpk>
    {
        [Key]
        public Tpk Id { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        
        public string ModifiedBy { get; set; }
        public Entity()
        {
            
        }

    }
}
