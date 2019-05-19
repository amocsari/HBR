using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entity
{
    [Table("Genre")]
    public class Genre
    {
        [Key]
        public string GenreId { get; set; }

        [Required]
        public string GenreName { get; set; }

        [Required]
        public bool Deleted { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public virtual ICollection<Book> Books { get; set; }
    }
}
