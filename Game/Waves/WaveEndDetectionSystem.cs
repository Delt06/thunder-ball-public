using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game._Data;
using Game.Enemies;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Waves
{
    public class WaveEndDetectionSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Enemy>> _enemiesFilter = default;
        private readonly EcsFilterInject<Inc<EnemyWave>, Exc<SpawnEnemyWaveCommand>> _filter = default;
        private readonly EcsFilterInject<Inc<Player.Player>> _playerFilter = default;
        private readonly StaticData _staticData;

        public WaveEndDetectionSystem(StaticData staticData) => _staticData = staticData;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                if (!_enemiesFilter.IsEmpty()) continue;
                if (_playerFilter.IsEmpty()) continue;

                ref var enemyWave = ref _filter.Pools.Inc1.Get(i);
                if (enemyWave.Index >= enemyWave.Total) continue;

                enemyWave.Index++;
                if (enemyWave.Index < enemyWave.Total)
                    Add<SpawnEnemyWaveCommand>(i).Delay = _staticData.WaveRespawnDelay;
                else
                    World.NewEntityWith<OnFinishedWaves>();
            }
        }
    }
}