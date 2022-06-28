using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemies.Specific
{
    [Serializable]
    public struct Teleportation
    {
        [Min(0f)]
        public float Period;
        [Min(0f)]
        public float Step;
        [HideInEditorMode]
        public float ElapsedTime;
    }
}