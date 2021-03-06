using System.Drawing;
using PKG1;

namespace Edelstein.Provider.Fields
{
    public class FieldLifeTemplate
    {
        public int TemplateID { get; set; }
        public FieldLifeType Type { get; set; }

        public byte F { get; set; }
        public Point Position { get; set; }
        public int RX0 { get; set; }
        public int RX1 { get; set; }
        public int FH { get; set; }

        public static FieldLifeTemplate Parse(WZProperty p)
        {
            var res = new FieldLifeTemplate
            {
                TemplateID = p.ResolveFor<int>("id") ?? -1,
                Type = p.ResolveForOrNull<string>("type").ToLower() == "n"
                    ? FieldLifeType.NPC
                    : FieldLifeType.Monster,
                F = (byte) (p.ResolveFor<bool>("f") ?? false ? 0 : 1),
                Position = new Point(
                    p.ResolveFor<int>("x") ?? int.MinValue,
                    p.ResolveFor<int>("y") ?? int.MinValue
                ),
                RX0 = p.ResolveFor<int>("rx0") ?? int.MinValue,
                RX1 = p.ResolveFor<int>("rx1") ?? int.MinValue,
                FH = p.ResolveFor<int>("fh") ?? 0
            };

            return res;
        }
    }
}