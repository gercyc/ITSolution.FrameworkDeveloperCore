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
    public class ApplicationUser : IdentityUser<int>
    {
        //for json
        [NotMapped]
        public string Password { get; set; }
        //for json
        [NotMapped]
        public string NewPassword { get; set; }

        public string Endereco { get; set; }
        public string Ancord { get; set; }
        public DateTime? DataDesativado { get; set; }

        public ApplicationUser()
        {
        }
    }
}
