using System.ComponentModel.DataAnnotations;

namespace RoleBased.ViewModels
{
    public class VerifyViewModel
    {

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
