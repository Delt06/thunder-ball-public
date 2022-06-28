using DELTation.LeoEcsExtensions.Systems.Run;
using Game.Ball;
using Game.Loot;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Skills.Freeze
{
    public class FreezingStartSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Ball.Ball, OwnerRef>> _ballFilter = default;
        private readonly EcsFilterInject<Inc<OnBallDealtDamage>> _onBallDealtDamageFilter = default;
        private readonly EcsFilterInject<Inc<FreezingAttack>> _ownerFilter = default;
        private readonly EcsFilterInject<Inc<CanBeFrozen>> _targetFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _onBallDealtDamageFilter)
            {
                ref var onBallDealtDamage = ref _onBallDealtDamageFilter.Pools.Inc1.Get(i);
                if (!_targetFilter.Contains(onBallDealtDamage.To, out var targetIdx)) continue;
                if (!_ballFilter.Contains(onBallDealtDamage.Ball, out var ballIdx)) continue;

                var owner = _ballFilter.Pools.Inc2.Get(ballIdx).Owner;
                if (!_ownerFilter.Contains(owner, out var ownerIdx)) continue;

                ref readonly var freezingAttack = ref _ownerFilter.Pools.Inc1.Get(ownerIdx);
                if (!RandomUtils.TryProbability(freezingAttack.Params.Probability)) continue;

                ref var isFrozen = ref GetOrAdd<IsFrozen>(targetIdx);
                isFrozen.RemainingTime = freezingAttack.Params.Duration;
                GetOrAdd<OnFroze>(targetIdx);
            }
        }
    }
}