using System.Drawing;
using Edelstein.Network.Packets;
using Edelstein.WvsGame.Fields.Objects.Users;
using Edelstein.WvsGame.Packets;

namespace Edelstein.WvsGame.Fields.Objects.Drops
{
    public abstract class FieldDrop : FieldObj
    {
        public override FieldObjType Type => FieldObjType.Drop;
        
        public abstract bool IsMoney { get; }
        public abstract int Info { get; }

        public OutPacket GetEnterFieldPacket(
            byte enterType,
            FieldObj source = null,
            short delay = 0
        )
        {
            using (var p = new OutPacket(GameSendOperations.DropEnterField))
            {
                p.Encode<byte>(enterType); // nEnterType
                p.Encode<int>(ID);

                p.Encode<bool>(IsMoney);
                p.Encode<int>(Info);
                p.Encode<int>(0); // dwOwnerID
                p.Encode<byte>(0x2); // nOwnType
                p.Encode<Point>(Position);
                p.Encode<int>(source is FieldUser ? 0 : source?.ID ?? 0); // dwSourceID

                if (enterType == 0x0 ||
                    enterType == 0x1 ||
                    enterType == 0x3 ||
                    enterType == 0x4)
                {
                    p.Encode<Point>(source?.Position ?? new Point(0, 0));
                    p.Encode<short>(delay);
                }

                if (!IsMoney)
                    p.Encode<long>(0);

                p.Encode<bool>(false);
                p.Encode<bool>(false);
                return p;
            }
        }

        public OutPacket GetLeaveFieldPacket(
            byte leaveType,
            FieldObj source = null
        )
        {
            using (var p = new OutPacket(GameSendOperations.DropLeaveField))
            {
                p.Encode<byte>(leaveType); // nLeaveType
                p.Encode<int>(ID);

                if (leaveType == 0x2 ||
                    leaveType == 0x3 ||
                    leaveType == 0x5) p.Encode<int>(source?.ID ?? 0);
                else if (leaveType == 0x4) p.Encode<short>(0);

                return p;
            }
        }

        public override OutPacket GetEnterFieldPacket()
        {
            return GetEnterFieldPacket(0x2);
        }

        public override OutPacket GetLeaveFieldPacket()
        {
            return GetLeaveFieldPacket(0x1);
        }

        public abstract void PickUp(FieldUser user);
    }
}