using System.Collections.Generic;
using Edelstein.Provider;
using Edelstein.Provider.Fields;
using Edelstein.Provider.Mobs;
using Edelstein.Provider.NPC;
using Edelstein.WvsGame.Fields.Objects;
using MoreLinq.Extensions;

namespace Edelstein.WvsGame.Fields
{
    public class FieldFactory
    {
        private readonly LazyTemplateManager<FieldTemplate> _fieldLazyTemplateManager;
        private readonly LazyTemplateManager<NPCTemplate> _npcLazyTemplateManager;
        private readonly LazyTemplateManager<MobTemplate> _mobLazyTemplateManager;
        private readonly IDictionary<int, Field> _fields;

        public FieldFactory(
            LazyTemplateManager<FieldTemplate> fieldLazyTemplateManager,
            LazyTemplateManager<NPCTemplate> npcLazyTemplateManager,
            LazyTemplateManager<MobTemplate> mobLazyTemplateManager
        )
        {
            _fieldLazyTemplateManager = fieldLazyTemplateManager;
            _npcLazyTemplateManager = npcLazyTemplateManager;
            _mobLazyTemplateManager = mobLazyTemplateManager;
            _fields = new Dictionary<int, Field>();
        }

        public Field Get(int templateId)
        {
            lock (this)
            {
                if (_fields.ContainsKey(templateId)) return _fields[templateId];
                var field = new Field(templateId, _fieldLazyTemplateManager.Get(templateId));
                _fields[templateId] = field;

                field.Template.Life.ForEach(l =>
                {
                    switch (l.Type)
                    {
                        case FieldLifeType.NPC:
                            var npcTemplate = _npcLazyTemplateManager.Get(l.TemplateID);

                            field.Enter(new FieldNPC(npcTemplate)
                            {
                                X = (short) l.X,
                                Y = (short) l.Y,
                                MoveAction = l.F,
                                Foothold = (short) l.FH,
                                RX0 = l.RX0,
                                RX1 = l.RX1
                            });
                            break;
                        case FieldLifeType.Monster:
                            var mobTemplate = _mobLazyTemplateManager.Get(l.TemplateID);

                            field.Enter(new FieldMob(mobTemplate)
                            {
                                X = (short) l.X,
                                Y = (short) l.Y,
                                MoveAction = l.F,
                                Foothold = (short) l.FH
                            });
                            break;
                    }
                });
                return field;
            }
        }
    }
}