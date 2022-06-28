using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Game.Waves
{
    public class WaveInitSystem : EcsSystemBase, IEcsInitSystem
    {
        public void Init(EcsSystems systems)
        {
            var entity = World.NewEntity();
            ref var enemyWave = ref Add<EnemyWave>(entity);
            enemyWave.Index = 0;
            enemyWave.Total = int.MaxValue;

            Add<SpawnEnemyWaveCommand>(entity);
        }
    }
}