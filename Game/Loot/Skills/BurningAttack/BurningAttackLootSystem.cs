namespace Game.Loot.Skills.BurningAttack
{
    public class BurningAttackLootSystem : SkillLootSystemBase<Game.Skills.Burning.BurningAttack, BurningAttackLoot>
    {
        protected override void Apply(in BurningAttackLoot loot, ref Game.Skills.Burning.BurningAttack skill)
        {
            skill.Params = loot.Params;
        }
    }
}