using System.Collections.Generic;
using Edelstein.Database.Entities;
using Edelstein.Network.Packets;

namespace Edelstein.WvsGame.Fields.Attacking
{
    public class MeleeAttackInfo : AttackInfo
    {
        public MeleeAttackInfo(Character character) : base(character)
        {
        }

        public override void Decode(InPacket packet)
        {
            // TODO: im not sure about this,
            // but it doesn't throw exceptions anymore
            if (packet.Decode<byte>() == 0xFF) 
                packet.Buffer.SkipBytes(7);
            else
            {
                packet.Decode<int>();
                packet.Decode<int>();
            }

            var damagePerMobAndCount = packet.Decode<byte>();
            DamagePerMob = damagePerMobAndCount & 0xF;
            Count = damagePerMobAndCount >> 4;

            packet.Decode<int>();
            packet.Decode<int>();
            SkillID = packet.Decode<int>();
            packet.Decode<byte>();
            packet.Decode<int>();
            packet.Decode<int>();
            packet.Decode<int>();
            packet.Decode<int>();
            // if Keydown - int // KeyDown
            packet.Decode<byte>();

            var attackActionAndIsLeft = packet.Decode<short>();
            AttackAction = (short) (attackActionAndIsLeft & 0x7FFF);
            IsLeft = ((attackActionAndIsLeft >> 15) & 1) != 0;

            packet.Decode<int>();
            AttackActionType = packet.Decode<byte>();
            AttackSpeed = packet.Decode<byte>();
            AttackTime = packet.Decode<int>();
            packet.Decode<int>();

            Entries = new List<AttackInfoEntry>();
            for (var i = 0; i < Count; i++)
            {
                var entry = new AttackInfoEntry();

                entry.Decode(packet, DamagePerMob);
                Entries.Add(entry);
            }

            packet.Decode<short>();
            packet.Decode<short>();
            // if Grenade - short, short // Position
        }
    }
}