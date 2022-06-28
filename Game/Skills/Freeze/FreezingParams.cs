using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Skills.Freeze
{
    [Serializable]
    public struct FreezingParams
    {
        [Min(0f)]
        public float Duration;
        [Range(0f, 1f)]
        public float Probability;
        [Min(0)]
        public int Level;

        [ShowInInspector]
        private float AverageFreezeTime => Duration * Probability;
    }
}