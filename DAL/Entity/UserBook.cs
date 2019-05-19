using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    [Table("UserBook")]
    public class UserBook
    {
        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required]
        public string UserIdentifier { get; set; }

        [Required]
        public int Progress { get; set; }

        [Required]
        public bool Deleted { get; set; }

        public virtual Book Book { get; set; }
    }
}
