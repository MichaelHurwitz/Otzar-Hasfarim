using System.ComponentModel.DataAnnotations;

namespace Library.ViewModel
{
    public class BookVm
    {
        public long Id { get; set; }

        [Required, StringLength(50, MinimumLength = 4)]
        public string Genre { get; set; } = string.Empty;

        [Required, StringLength(50, MinimumLength = 5)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int Height { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public long SetId { get; set; }
    }
}
