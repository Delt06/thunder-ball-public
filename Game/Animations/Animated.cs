using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Animations
{
    [Serializable]
    public struct Animated
    {
        [Required]
        public Animator Animator;
    }
}