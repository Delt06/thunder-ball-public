using System;
using UnityEngine;

namespace Game.Animations.Enemy
{
    [Serializable]
    public struct EnemyAnimationParams
    {
        [Min(0f)]
        public float MovementSpeedMultiplier;
        [Min(1)]
        public int AttackTypes;
    }
}