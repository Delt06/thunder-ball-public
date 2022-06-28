using Game.Skills;
using Game.Skills.Crit;

namespace Game.Loot.Skills.Crit
{
    public struct CritAttackLoot : IHasLevel
    {
        public CritParams Params;
        public int Level => Params.Level;
    }
}