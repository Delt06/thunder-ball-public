using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Health.Bar
{
    [Serializable]
    public struct HealthBarPlaceholder
    {
        public enum HealthBarType
        {
            Default,
            Player,
        }

        public HealthBarType Type;
        [HideInEditorMode]
        public Transform Transform;
    }
}