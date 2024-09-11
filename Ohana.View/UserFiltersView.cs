using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ohana.View
{
    [Table("userfilterview", Schema = "ohanasafeguard")]
    public class UserFiltersView 
    { 
        [Column("id")]
        public int FilterId { get; set; }
        [Column("userrow")]
        public string? UserRow { get; set; }
        [Column("name")]
        public string? FilterName { get; set; }
        [Column("type")]
        public bool FilterType { get; set; }

    }
}
