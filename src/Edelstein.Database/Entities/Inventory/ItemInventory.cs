using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edelstein.Database.Entities.Inventory
{
    public class ItemInventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public ICollection<ItemSlot> Items { get; set; } = new List<ItemSlot>();
        public byte SlotMax { get; set; }

        public ItemInventory(byte slotMax)
        {
            this.SlotMax = slotMax;
        }
    }
}