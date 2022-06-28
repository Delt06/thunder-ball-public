using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Utilities;
using Game.Enemies;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Waves.Difficulty
{
    public class DifficultyService
    {
        private readonly EcsSingletonPool<EnemyWave> _enemyWavePool;
        private readonly ProgressionConfig _progressionConfig;

        public DifficultyService(EcsWorld world, ProgressionConfig progressionConfig)
        {
            _progressionConfig = progressionConfig;
            _enemyWavePool = world.GetSingletonPool<EnemyWave>();
        }

        public void ApplyDifficultyToEnemyOnce(EcsPackedEntityWithWorld entity)
        {
            var waveIndex = _enemyWavePool.Get().Index;

            ref var healthData = ref entity.Modify<HealthData>();
            var ratio = healthData.Health / healthData.MaxHealth;
            healthData.MaxHealth *= Mathf.Pow(_progressionConfig.EnemyHealthFactor, waveIndex);
            healthData.Health = healthData.MaxHealth * ratio;

            ref var attack = ref entity.Get<Attack>();
            attack.Damage *= Mathf.Pow(_progressionConfig.EnemyDamageFactor, waveIndex);
        }

        public void ApplyDifficultyToPlayerIncremental(ref Player.Player player)
        {
            player.BallSpeed *= _progressionConfig.BallSpeedFactor;
        }
    }
}