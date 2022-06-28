using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Animations.Player
{
    [Serializable]
    public struct PlayerAnimationParams
    {
        [Min(0f)]
        public float MovementDirectionSmoothTime;
        [Min(1)]
        public int AttackTypes;
        [HideInEditorMode]
        public float MovementDirectionVelocity;
    }
}