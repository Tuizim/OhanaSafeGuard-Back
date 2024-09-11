using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Ohana.Domain
{
    [Table("credentialstorage", Schema = "ohanasafeguard")]
    public class CredentialStorage
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        [Column("login")]
        public string? Login { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        [Column("url")]
        public string? Url { get; set; }
        [Column("userrow")]
        public string? UserRow { get; set; }
        [Column("filter_id")]
        public int Filter {  get; set; }
    }
}
