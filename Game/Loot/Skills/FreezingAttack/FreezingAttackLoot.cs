using Game.Skills;
using Game.Skills.Freeze;

namespace Game.Loot.Skills.FreezingAttack
{
    public struct FreezingAttackLoot : IHasLevel
    {
        public FreezingParams Params;

        public int Level => Params.Level;
    }
}