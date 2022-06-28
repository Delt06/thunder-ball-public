using Game.Skills;
using Game.Skills.Burning;

namespace Game.Loot.Skills.BurningAttack
{
    public struct BurningAttackLoot : IHasLevel
    {
        public BurningParams Params;

        public int Level => Params.Level;
    }
}