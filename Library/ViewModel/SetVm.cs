using System.ComponentModel.DataAnnotations;

namespace Library.ViewModel
{
    public class SetVm
    {
        public long Id { get; set; }

        [Required, StringLength(30, MinimumLength = 4)]
        public string SetName { get; set; } = string.Empty;

        [Required]
        public long ShelfId { get; set; }
    }
}
