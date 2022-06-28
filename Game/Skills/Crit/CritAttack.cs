using System;
using Sirenix.OdinInspector;

namespace Game.Skills.Crit
{
    [Serializable]
    public struct CritAttack : ISkill
    {
        [HideLabel] [InlineProperty]
        public CritParams Params;
        public int Level => Params.Level;
    }
}