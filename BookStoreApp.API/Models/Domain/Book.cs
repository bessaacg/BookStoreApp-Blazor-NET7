using BookStoreApp.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApp.API.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Configure Id as identity column
        public int Id { get; set; }
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
