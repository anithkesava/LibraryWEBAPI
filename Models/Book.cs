using System.ComponentModel.DataAnnotations;
namespace LibraryServicesAPI.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public int Page { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int BookCount{ get; set; }
    }
}
