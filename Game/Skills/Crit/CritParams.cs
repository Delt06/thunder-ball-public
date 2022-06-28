using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Skills.Crit
{
    [Serializable]
    public struct CritParams
    {
        [Min(0)]
        public int Level;
        [Min(0f)]
        public float ExtraMultiplier;
        [Range(0f, 1f)]
        public float Probability;

        [ShowInInspector]
        private float AverageExtraDamage => ExtraMultiplier * Probability;
    }
}