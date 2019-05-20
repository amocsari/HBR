using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    [Table("UserBook")]
    public class UserBook
    {
        [Key]
        public int UserBookId { get; set; }
        [Required]
        [ForeignKey("Book")]
        public string BookId { get; set; }

        [Required]
        public string UserIdentifier { get; set; }

        [Required]
        public int Progress { get; set; }

        [Required]
        public bool Deleted { get; set; }

        public virtual Book Book { get; set; }
    }
}
