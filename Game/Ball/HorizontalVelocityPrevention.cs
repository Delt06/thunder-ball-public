using System;
using UnityEngine;

namespace Game.Ball
{
    [Serializable]
    public struct HorizontalVelocityPrevention
    {
        [Range(0f, 1f)]
        public float MinDot;
        [Min(0f)]
        public float FixRotation;
    }
}