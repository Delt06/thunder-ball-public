using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Cameras
{
    [Serializable]
    public struct LookAtCamera
    {
        [Required]
        public Transform Transform;
    }
}