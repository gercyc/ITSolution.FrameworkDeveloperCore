using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ITSolution.Framework.Core.Common.BaseInterfaces;

public interface IEntity<TPk>
{
    [Key]
    public TPk Id { get; set; }
    [JsonIgnore]
    public DateTime CreatedTimestamp { get; set; }
    [JsonIgnore]
    public DateTime? ModifiedDate { get; set; }
    [JsonIgnore]
    public int? CreatedBy { get; set; }
    [JsonIgnore]
    public int? ModifiedBy { get; set; }
}