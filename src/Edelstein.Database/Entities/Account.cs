using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Edelstein.Database.Entities.Types;

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

        public AccountState State { get; set; }

        public ICollection<AccountData> Data { get; set; }
    }
}