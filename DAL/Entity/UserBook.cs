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
        public int BookId { get; set; }

        [Required]
        //TODO: foreign key to user
        public int UserId { get; set; }

        [Required]
        public int Progress { get; set; }

        [Required]
        public bool Deleted { get; set; }
    }
}
