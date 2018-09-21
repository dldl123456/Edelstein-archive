using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edelstein.Database.Entities
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(13)] public string Username { get; set; }
        [MaxLength(128)] public string Password { get; set; }
        
        [MaxLength(128)] public string SPW { get; set; }

        public ICollection<AccountData> Data { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}