using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ohana.Domain
{
    [Table("defaultfilter", Schema = "ohanasafeguard")]
    public class DefaultFilter
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

    }
}
