using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game.Destruction;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Ball
{
    public class BallCatchingSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Ball, OwnerRef>> _ballFilter = default;
        private readonly EcsFilterInject<Inc<OnBallCollided>> _filter = default;
        private readonly EcsFilterInject<Inc<Player.Player>> _playerFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var onBallCollided = _filter.Pools.Inc1.Get(i);
                if (!_ballFilter.Contains(onBallCollided.Ball, out var ballIdx)) continue;
                if (!_playerFilter.Contains(onBallCollided.Other)) continue;

                var owner = _ballFilter.Pools.Inc2.Get(ballIdx).Owner;
                if (owner.IsAlive())
                    owner.Add<OnBallCaught>();

                GetOrAdd<EntityDestructionCommand>(ballIdx);
            }
        }
    }
}