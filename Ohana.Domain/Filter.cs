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
        
        [Column("userrow")]
        public string? UserRow { get; set; }
        [Column("type")]
        public bool Type { get; set; }

    }
}
