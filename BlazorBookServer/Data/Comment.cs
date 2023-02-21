using System.ComponentModel.DataAnnotations;

namespace BlazorBookApp.Data
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
