using System;
using UnityEngine;

namespace Game.Ball
{
    [Serializable]
    public struct BaseBallDamage
    {
        [Min(0f)]
        public float Damage;
    }
}