using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ohana.View
{
    [Table("userfilterview", Schema = "ohanasafeguard")]
    public class UserFiltersView 
    { 
        [Column("filter_id")]
        public int FilterId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("filter_name")]
        public string? FilterName { get; set; }
        [Column("filter_type")]
        public string? FilterType { get; set; }
    }
}
