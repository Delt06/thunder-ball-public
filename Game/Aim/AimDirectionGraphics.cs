using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Aim
{
    [Serializable]
    public struct AimDirectionGraphics
    {
        [Required]
        public Transform Transform;
        [Required]
        public GameObject Model;
    }
}