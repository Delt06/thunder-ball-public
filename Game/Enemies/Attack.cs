using System;
using UnityEngine;

namespace Game.Enemies
{
    [Serializable]
    public struct Attack
    {
        [Min(0f)]
        public float Damage;
        [Min(0f)]
        public float Duration;
        [Min(0f)]
        public float Cooldown;
        [Range(0f, 1f)]
        public float NormalizedDealDamageTime;
    }
}