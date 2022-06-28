using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Skills.Burning
{
    [Serializable]
    public struct BurningParams
    {
        [Min(0)]
        public int Level;
        [Min(0)]
        public float Damage;
        [Min(0)]
        public float DamagePeriod;
        [Min(1)]
        public int DamageTimes;

        [ShowInInspector]
        public float TotalDamage => Damage * DamageTimes;

        [ShowInInspector]
        public float TotalDuration => DamagePeriod * DamageTimes;

        [ShowInInspector]
        public float Dps => TotalDamage / TotalDuration;
    }
}