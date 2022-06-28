using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Destruction
{
    [Serializable]
    public struct CreateDeadBody
    {
        public bool HasLifetime;
        [ShowIf(nameof(HasLifetime))] [Min(0f)]
        public float Lifetime;
    }
}