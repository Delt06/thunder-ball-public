using System;
using UnityEngine;

namespace Game.Skills.Freeze
{
    [Serializable]
    public struct IsFrozen
    {
        [Min(0f)]
        public float RemainingTime;
    }
}