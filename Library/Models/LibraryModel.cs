using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    [Index(nameof(Genre), IsUnique = true)]

    public class LibraryModel
    {
        public long Id { get; set; }

        [Required, StringLength(50, MinimumLength =4)]
        public string Genre { get; set; } = string.Empty;
        public List<ShelfModel> Shelves { get; set; } = [];
    }
}
