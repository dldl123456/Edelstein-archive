using Edelstein.Database.Entities.Inventory;
using Edelstein.Network.Packets;

namespace Edelstein.Common.Packets.Inventory.Operations
{
    public class InventoryAddOperation : InventoryOperation
    {
        private readonly ItemSlot _item;

        public InventoryAddOperation(ItemInventoryType inventory, ItemSlot item)
            : base(InventoryOperationType.Add, inventory, item.Position)
        {
            _item = item;
        }

        public override void Encode(OutPacket packet)
        {
            base.Encode(packet);

            switch (_item)
            {
                case ItemSlotEquip equip:
                    equip.Encode(packet);
                    break;
                case ItemSlotBundle bundle:
                    bundle.Encode(packet);
                    break;
            }
        }
    }
}