using System;
using Game.Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Waves
{
    [CreateAssetMenu]
    public class WavePreset : ScriptableObject
    {
        [SerializeField] [ListDrawerSettings(Expanded = true)]
        private EnemyPlacement[] _enemyPlacements;

        public EnemyPlacement[] EnemyPlacements => _enemyPlacements;

        [Serializable]
        public struct EnemyPlacement
        {
            [Required] [AssetSelector]
            public EnemyPreset Enemy;
            [Range(0f, 1f)]
            public float X;
            [Min(0f)]
            public float Z;
        }
    }
}