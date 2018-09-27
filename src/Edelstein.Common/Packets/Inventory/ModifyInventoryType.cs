namespace Edelstein.Common.Packets.Inventory
{
    public enum ModifyInventoryType
    {
        Equip = 0x1,
        Use = 0x2,
        Setup = 0x3,
        Etc = 0x4,
        Cash = 0x5,

        Equipped = 0xFF
    }
}