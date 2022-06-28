using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Waves
{
    [CreateAssetMenu]
    public class ProgressionConfig : ScriptableObject
    {
        [SerializeField] [Required] [AssetSelector]
        private WavePreset[] _startPresets;

        [SerializeField] [Required] [AssetSelector]
        private WavePreset[] _mainPresets;

        [SerializeField] [Min(0f)] private float _enemyHealthFactor = 1.1f;
        [SerializeField] [Min(0f)] private float _enemyDamageFactor = 1.1f;
        [SerializeField] [Min(0f)] private float _ballSpeedFactor = 1.05f;

        public WavePreset[] StartPresets => _startPresets;

        public WavePreset[] MainPresets => _mainPresets;

        public float EnemyHealthFactor => _enemyHealthFactor;
        public float EnemyDamageFactor => _enemyDamageFactor;
        public float BallSpeedFactor => _ballSpeedFactor;
    }
}