using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemies
{
    [Serializable]
    public struct EnemyMovement
    {
        [Min(0f)]
        public float Speed;
        public Vector3 Direction;
        [Min(0f)]
        public float DesiredDistance;
        public bool AutoMovement;
        [HideInEditorMode]
        public bool ReachedDesiredDistance;
    }
}