using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class BookModel
    {
        public long Id { get; set; }

        [Required, StringLength(50, MinimumLength = 4)]
        public required string Genre { get; set; }

        [Required, StringLength(50, MinimumLength = 5)] 
        public required string Title { get; set; }
        [Required]
        public required int Height { get; set; }
        [Required]
        public required int Width { get; set; }
        public SetModel? Set { get; set; }
        public long SetId { get; set; }

    }
}