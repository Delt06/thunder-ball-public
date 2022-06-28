namespace Game.Loot.Skills.FreezingAttack
{
    public class FreezingAttackLootSystem : SkillLootSystemBase<Game.Skills.Freeze.FreezingAttack, FreezingAttackLoot>
    {
        protected override void Apply(in FreezingAttackLoot loot, ref Game.Skills.Freeze.FreezingAttack skill)
        {
            skill.Params = loot.Params;
        }
    }
}