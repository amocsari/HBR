using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string Isbn { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [ForeignKey("Genre")]
        public int? GenreId { get; set; }

        [Required]
        public bool Deleted { get; set; }

        [Required]
        public string Extension { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        [Required]
        public string UploaderIdentifier { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
    }
}
