using DELTation.LeoEcsExtensions.Systems.Run;
using Game.Ball;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Loot.Collection
{
    public class LootCollectionStartSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Ball.Ball, OwnerRef>> _ballFilter = default;
        private readonly EcsFilterInject<Inc<OnBallCollided>> _filter = default;
        private readonly EcsFilterInject<Inc<Loot>, Exc<IsBeingCollected>> _lootFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var onBallCollided = _filter.Pools.Inc1.Get(i);
                if (!_lootFilter.Contains(onBallCollided.Other, out var lootIdx)) continue;
                if (!_ballFilter.Contains(onBallCollided.Ball, out var ballIdx)) continue;

                var collector = _ballFilter.Pools.Inc2.Get(ballIdx).Owner;
                Add<IsBeingCollected>(lootIdx).Collector = collector;
            }
        }
    }
}