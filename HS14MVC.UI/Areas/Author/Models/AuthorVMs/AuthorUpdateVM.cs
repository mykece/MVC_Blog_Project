using System.ComponentModel.DataAnnotations;

namespace HS14MVC.UI.Areas.Author.Models.AuthorVMs
{
    public class AuthorUpdateVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
