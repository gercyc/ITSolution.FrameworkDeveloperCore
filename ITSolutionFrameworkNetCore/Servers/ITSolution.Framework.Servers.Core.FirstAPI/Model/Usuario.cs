using ITSolution.Framework.Common.BaseClasses.CommonEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITSolution.Framework.Servers.Core.FirstAPI.Model
{
    [Table("Usuario")]
    public class Usuario : AbstractUser
    {
        [Key]//pk
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//sera auto increment

        public int IdUsuario { get; set; }
    }
}
