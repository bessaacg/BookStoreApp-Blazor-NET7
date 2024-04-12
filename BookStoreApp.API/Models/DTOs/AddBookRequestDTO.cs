using BookStoreApp.API.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApp.API.Models.DTOs
{
    public class AddBookRequestDTO
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal? Price { get; set; } // Nullable decimal
        public int? AuthorId { get; set; } // Nullable int

        // Navigation property
        public Author Author { get; set; }
    }
}
