using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Cameras.Shake
{
    [Serializable]
    [InlineProperty]
    public struct CameraShakePreset
    {
        [Min(0f)]
        public float Amplitude;
        [Min(0f)]
        public float Duration;
        public Ease Ease;

        public static CameraShakePreset Standard => new()
        {
            Amplitude = 1f,
            Duration = 0.1f,
            Ease = Ease.OutSine,
        };
    }
}