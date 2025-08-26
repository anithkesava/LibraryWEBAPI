using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace LibraryServicesAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public UserType UserType { get; set; }
        public int BorrowedBookCount { get; set; } = 0;
        public int ReturnBookCount { get; set; } = 0;
        public int MaximumBorrowLimit { get; set; }
        public List<string>? BorrowedBookList { get; set; }
    }
    public enum UserType
    {
        Admin,
        Member,
        Student,
    }
}
