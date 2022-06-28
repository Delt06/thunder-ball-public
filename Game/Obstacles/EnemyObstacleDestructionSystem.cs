using DELTation.LeoEcsExtensions.Systems.Run;
using Game.Destruction;
using Game.Enemies;
using Game.Loot.Drop;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Obstacles
{
    public class EnemyObstacleDestructionSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnEnemyContact>> _contactFilter = default;
        private readonly EcsFilterInject<Inc<Obstacle>> _obstacleFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _contactFilter)
            {
                var other = _contactFilter.Pools.Inc1.Get(i).Other;
                if (!_obstacleFilter.Contains(other, out var obstacleIdx)) continue;

                Del<DropOnDestruction>(obstacleIdx);
                GetOrAdd<EntityDestructionCommand>(obstacleIdx);
            }
        }
    }
}