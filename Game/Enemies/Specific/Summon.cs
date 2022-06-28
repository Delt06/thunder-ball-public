using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemies.Specific
{
    [Serializable]
    public struct Summon
    {
        [Required] [AssetSelector]
        public EnemyPreset EnemyPreset;
        [Min(0f)] [MinMaxSlider(0, 10, true)]
        public Vector2 DistanceRange;
        [Min(0f)]
        public float Period;
        [HideInEditorMode]
        public float ElapsedTime;
    }
}