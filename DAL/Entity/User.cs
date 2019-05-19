using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class User
    {
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
