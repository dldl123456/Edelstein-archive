using org.mariuszgromada.math.mxparser;
using PKG1;

namespace Edelstein.Provider.Skills
{
    public class SkillLevelTemplate
    {
        public short HP { get; set; }
        public short MP { get; set; }
        public short PAD { get; set; }
        public short PDD { get; set; }
        public short MAD { get; set; }
        public short MDD { get; set; }
        public short ACC { get; set; }
        public short EVA { get; set; }
        public short Craft { get; set; }
        public short Speed { get; set; }
        public short Jump { get; set; }

        public short Morph { get; set; }
        public short HPCon { get; set; }
        public short MPCon { get; set; }
        public short MoneyCon { get; set; }
        public short ItemCon { get; set; }
        public short ItemConNo { get; set; }
        public short Damage { get; set; }
        public short FixDamage { get; set; }
        public short SelfDesctruction { get; set; }

        public short Time { get; set; }
        public short SubTime { get; set; }

        public short Prop { get; set; }
        public short SubProp { get; set; }

        public short AttackCount { get; set; }
        public short BulletCount { get; set; }
        public short BulletConsume { get; set; }
        public short Mastery { get; set; }
        public short MobCount { get; set; }

        public short X { get; set; }
        public short Y { get; set; }
        public short Z { get; set; }

        public short EMHP { get; set; }
        public short EMMP { get; set; }
        public short EPAD { get; set; }
        public short EMAD { get; set; }
        public short EPDD { get; set; }
        public short EMDD { get; set; }

        public static SkillLevelTemplate Parse(int level, WZProperty p)
        {
            var x = new Argument("x", level);

            var u = new Function("u", "ceil(x)", "x");
            var d = new Function("d", "floor(x)", "x");

            return new SkillLevelTemplate
            {
                HP = ResolveExpression(p.ResolveForOrNull<string>("hp"), x, u, d),
                MP = ResolveExpression(p.ResolveForOrNull<string>("mp"), x, u, d),
                PAD = ResolveExpression(p.ResolveForOrNull<string>("pad"), x, u, d),
                PDD = ResolveExpression(p.ResolveForOrNull<string>("pdd"), x, u, d),
                MAD = ResolveExpression(p.ResolveForOrNull<string>("mad"), x, u, d),
                MDD = ResolveExpression(p.ResolveForOrNull<string>("mdd"), x, u, d),
                ACC = ResolveExpression(p.ResolveForOrNull<string>("acc"), x, u, d),
                EVA = ResolveExpression(p.ResolveForOrNull<string>("eva"), x, u, d),
                Craft = ResolveExpression(p.ResolveForOrNull<string>("craft"), x, u, d),
                Speed = ResolveExpression(p.ResolveForOrNull<string>("speed"), x, u, d),
                Jump = ResolveExpression(p.ResolveForOrNull<string>("jump"), x, u, d),
                X = ResolveExpression(p.ResolveForOrNull<string>("x"), x, u, d),
                Y = ResolveExpression(p.ResolveForOrNull<string>("y"), x, u, d),
                Z = ResolveExpression(p.ResolveForOrNull<string>("z"), x, u, d),
                EMHP = ResolveExpression(p.ResolveForOrNull<string>("emhp"), x, u, d),
                EMMP = ResolveExpression(p.ResolveForOrNull<string>("emmp"), x, u, d),
                EPAD = ResolveExpression(p.ResolveForOrNull<string>("epad"), x, u, d),
                EMAD = ResolveExpression(p.ResolveForOrNull<string>("emad"), x, u, d),
                EPDD = ResolveExpression(p.ResolveForOrNull<string>("epdd"), x, u, d),
                EMDD = ResolveExpression(p.ResolveForOrNull<string>("emdd"), x, u, d)
            };
        }

        private static short ResolveExpression(string expression, params PrimitiveElement[] elements)
        {
            if (expression == null) return 0;
            return (short) new Expression(expression, elements).calculate();
        }
    }
}