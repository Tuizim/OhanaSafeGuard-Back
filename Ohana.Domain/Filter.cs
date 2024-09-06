using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ohana.Domain
{
    [Table("filter", Schema = "ohanasafeguard")]
    public class Filter
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }
        
        [Column("user_id")]
        public int UserId { get; set; }

    }
}
