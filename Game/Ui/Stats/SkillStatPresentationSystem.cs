using Game._Data;
using Game.Loot.Skills;
using Game.Skills;

namespace Game.Ui.Stats
{
    public class SkillStatPresentationSystem<TSkill> : StatPresentationSystem<TSkill>
        where TSkill : struct, ISkill
    {
        private readonly ISkillLoot _lootAssetBase;

        public SkillStatPresentationSystem(UiSceneData ui, ISkillLoot lootAssetBase) : base(ui) =>
            _lootAssetBase = lootAssetBase;


        protected override void Present(in TSkill stat, StatView view)
        {
            view
                .UpdateName(_lootAssetBase.SkillName)
                .UpdateInfoLevel(stat.Level);
        }
    }
}