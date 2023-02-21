using System.ComponentModel.DataAnnotations;

namespace BlazorBookApp.Data
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        public string Image { get; set; }
        public string Description { get; set; }
        public virtual List<Comment>? Comments { get; set; }
    }
}
