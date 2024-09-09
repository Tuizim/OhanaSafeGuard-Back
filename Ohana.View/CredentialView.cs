using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ohana.View
{
    [Table("credentialview", Schema = "ohanasafeguard")]
    public class CredentialView
    {
        [Column("name")]
        public string? CredentialName { get; set; }
        [Column("filter_name")]
        public string? FIlterName { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
    }
}
