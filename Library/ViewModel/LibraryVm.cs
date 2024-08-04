using System.ComponentModel.DataAnnotations;

namespace Library.ViewModel
{
    public class LibraryVm
    {
        public long Id { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string Genre { get; set; } = string.Empty;
    }
}
