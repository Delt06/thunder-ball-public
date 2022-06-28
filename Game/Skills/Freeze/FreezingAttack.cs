using System;
using Sirenix.OdinInspector;

namespace Game.Skills.Freeze
{
    [Serializable]
    public struct FreezingAttack : ISkill
    {
        [HideLabel] [InlineProperty]
        public FreezingParams Params;

        public int Level => Params.Level;
    }
}