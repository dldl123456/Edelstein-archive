using PKG1;

namespace Edelstein.Provider.Mobs
{
    public class MobTemplate
    {
        public int TemplateID { get; set; }

        public short Level { get; set; }
        public int MaxHP { get; set; }
        public int MaxMP { get; set; }

        public static MobTemplate Parse(int templateId, PackageCollection collection)
        {
            var mobEntry = collection.Resolve($"Mob/{templateId:D7}.img");
            return Parse(templateId, mobEntry);
        }

        public static MobTemplate Parse(int templateId, WZProperty p)
        {
            return new MobTemplate
            {
                TemplateID = templateId,
                Level = p.ResolveFor<short>("info/level") ?? 0,
                MaxHP = p.ResolveFor<int>("info/maxHP") ?? 1,
                MaxMP = p.ResolveFor<int>("info/maxMP") ?? 0
            };
        }
    }
}