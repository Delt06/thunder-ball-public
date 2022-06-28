using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemies.Specific
{
    [Serializable]
    public struct OscillatingMovement
    {
        [Min(0f)]
        public float Amplitude;
        [Min(0f)]
        public float Speed;
        public AnimationCurve Curve;
        [HideInEditorMode]
        public float StartX;
        [HideInEditorMode]
        public float Time;
    }
}