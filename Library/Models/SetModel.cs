using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class SetModel
    {
        public long Id { get; set; }
        [Required, StringLength(30,MinimumLength =4)]
        public required string SetName { get; set; }
        public ShelfModel? Shelf { get; set; }
        public long ShelfId {  get; set; }
        public List<BookModel> Books { get; set; } = [];
    }
}