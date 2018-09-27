namespace Edelstein.Common.Packets.Inventory
{
    public enum InventoryOperationType
    {
        Add = 0x0,
        UpdateQuantity = 0x1,
        Move = 0x2,
        Remove = 0x3,
        UpdateEXP = 0x4
    }
}