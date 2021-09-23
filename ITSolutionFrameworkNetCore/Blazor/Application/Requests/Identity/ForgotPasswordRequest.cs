using System.ComponentModel.DataAnnotations;

namespace ITSolution.Framework.Blazor.Application.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}