using ITSolution.Framework.Core.BaseClasses.Identity;
using Newtonsoft.Json;
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
        [JsonIgnore]
        public DateTime CreatedTimestamp { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }
        [JsonIgnore]
        public int? CreatedBy { get; set; }
        [JsonIgnore]
        public int? ModifiedBy { get; set; }
        public Entity()
        {

        }

    }
}
