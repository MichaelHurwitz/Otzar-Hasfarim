using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class ShelfModel
    {
        public long Id { get; set; }
        [Required]
        public required int Height { get; set; }
        [Required]
        public required int Width { get; set; }
        public LibraryModel? Library { get; set; }
        public long LibraryId { get; set; }
        public List<SetModel> Sets { get; set; } = [];

    }
}