using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITSolution.Framework.Core.BaseClasses.Identity
{
    //AspNetUsers
    //[Table("ITS_USER")]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Data Inclusão")]
        public DateTime DataInclusao { get; set; }

        [StringLength(200)]
        public string Skin { get; set; }

        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string NewPassword { get; set; }

        public ApplicationUser()
        {
        }
    }
}
