using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game.Destruction;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Ball
{
    public class BallDestructionSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Ball, OwnerRef>> _ballFilter = default;
        private readonly EcsFilterInject<Inc<Ball, OwnerRef, DestroyBallCommand>> _commandFilter = default;
        private readonly EcsFilterInject<Inc<OnBallCollided>> _filter = default;
        private readonly EcsFilterInject<Inc<BallDestructionZone>> _zoneFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var onBallCollided = _filter.Pools.Inc1.Get(i);
                if (!_ballFilter.Contains(onBallCollided.Ball, out var ballIdx)) continue;
                if (!_zoneFilter.Contains(onBallCollided.Other)) continue;

                GetOrAdd<DestroyBallCommand>(ballIdx);
            }

            foreach (var i in _commandFilter)
            {
                var owner = _commandFilter.Pools.Inc2.Get(i).Owner;
                if (owner.IsAlive())
                    owner.GetOrAdd<OnBallDestroyed>();
                GetOrAdd<EntityDestructionCommand>(i);
            }
        }
    }
}