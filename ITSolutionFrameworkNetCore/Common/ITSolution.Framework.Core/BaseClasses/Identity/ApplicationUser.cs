using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITSolution.Framework.Core.BaseClasses.Identity
{
    public class ApplicationUser : IdentityUser<string>
    {
        [Required]
        [Display(Name = "Data Inclusão")]
        public DateTime DataInclusao { get; set; }

        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string NewPassword { get; set; }

        public ApplicationUser()
        {
            if (Id == null)
                Id = Guid.NewGuid().ToString();
        }
    }
}
