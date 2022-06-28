using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Player
{
    [Serializable]
    public struct Player
    {
        [Required]
        public Transform Transform;
        [Range(0f, 90f)]
        public float MaxAngle;
        [Min(0f)]
        public float MovementSpeed;
        [Min(0f)]
        public float MovementSensitivity;
        [Min(0f)]
        public float BallSpawnDistance;
        [Min(0f)]
        public float BallSpeed;
        [HideInEditorMode]
        public Vector3 LastAimDirection;
        [HideInEditorMode]
        public float LastMovementDirection;
    }
}