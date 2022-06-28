using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game.Enemies;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Utils;

namespace Game.Ball
{
    public class BallEnemyDamageSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Ball, BallDamage>> _ballFilter = default;
        private readonly EcsFilterInject<Inc<OnBallCollided>> _filter = default;
        private readonly EcsFilterInject<Inc<HealthData, DamageableByBall>> _otherFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var onBallCollided = ref _filter.Pools.Inc1.Get(i);
                if (!_ballFilter.Contains(onBallCollided.Ball, out var ballIdx)) continue;
                if (!_otherFilter.Contains(onBallCollided.Other, out var otherIdx)) continue;

                var damage = _ballFilter.Pools.Inc2.Get(ballIdx).Damage;
                ref var destructibleTarget = ref _otherFilter.Pools.Inc1.Modify(otherIdx);
                destructibleTarget.Health -= damage;

                ref var onBallDealtDamage = ref World.NewEntityWith<OnBallDealtDamage>();
                onBallDealtDamage.Damage = damage;
                onBallDealtDamage.Ball = onBallCollided.Ball;
                onBallDealtDamage.To = onBallCollided.Other;
            }
        }
    }
}