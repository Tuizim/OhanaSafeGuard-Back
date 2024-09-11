using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ohana.Domain
{
    [Table("user", Schema = "ohanasafeguard")]
    public class User
    {
        [Key]
        [Column("id")] 
        public int Id { get; set; }

        [Column("login")]
        public string? Login { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("userrow")]
        public string? UserRow { get; set; }
    }
}
