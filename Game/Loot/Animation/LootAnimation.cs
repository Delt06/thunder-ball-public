using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Loot.Animation
{
    [Serializable]
    public struct LootAnimation
    {
        [Required]
        public Transform Transform;
        public AnimationCurve Curve;
        [Min(0f)]
        public float Speed;
        [HideInEditorMode]
        public float ElapsedTime;
    }
}