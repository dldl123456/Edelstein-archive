using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edelstein.Database.Entities.Inventory
{
    public abstract class ItemSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public ItemInventory ItemInventory { get; set; }

        public short Position { get; set; }

        public int TemplateID { get; set; }
        public DateTime? DateExpire { get; set; }
    }
}