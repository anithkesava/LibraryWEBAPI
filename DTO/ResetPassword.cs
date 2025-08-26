using System.ComponentModel.DataAnnotations;

namespace LibraryServicesAPI.DTO
{
    public class ResetPassword
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ReEnterNewPassword{ get; set; }
    }
}
