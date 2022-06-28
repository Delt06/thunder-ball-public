using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Ball
{
    [Serializable]
    public struct Ball
    {
        [Required]
        public Rigidbody Rigidbody;
        [HideInEditorMode]
        public float Speed;
    }
}