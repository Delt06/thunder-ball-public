using System;
using Sirenix.OdinInspector;

namespace Game.Skills.Burning
{
    [Serializable]
    public struct BurningAttack : ISkill
    {
        [InlineProperty] [HideLabel]
        public BurningParams Params;

        public int Level => Params.Level;
    }
}