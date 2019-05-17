using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entity
{
    [Table("Bookmark")]
    public class Bookmark
    {
        [Key]
        public int BookmarkId { get; set; }

        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required]
        //TODO foreign key user-re
        public int UserId { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [Required]
        public bool Deleted { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
