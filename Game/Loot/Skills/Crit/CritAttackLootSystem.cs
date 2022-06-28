using Game.Skills.Crit;

namespace Game.Loot.Skills.Crit
{
    public class CritAttackLootSystem : SkillLootSystemBase<CritAttack, CritAttackLoot>
    {
        protected override void Apply(in CritAttackLoot loot, ref CritAttack skill)
        {
            skill.Params = loot.Params;
        }
    }
}