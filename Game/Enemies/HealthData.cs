using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemies
{
    [Serializable]
    public struct HealthData
    {
        [Min(0f)]
        public float Health;
        [HideInEditorMode]
        public float MaxHealth;
    }
}