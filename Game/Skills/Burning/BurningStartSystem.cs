using DELTation.LeoEcsExtensions.Systems.Run;
using Game.Ball;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Skills.Burning
{
    public class BurningStartSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OwnerRef>> _ballFilter = default;
        private readonly EcsFilterInject<Inc<BurningAttack>> _damageDealerFilter = default;
        private readonly EcsFilterInject<Inc<OnBallDealtDamage>> _eventFilter = default;
        private readonly EcsFilterInject<Inc<CanBurn>> _targetFilter = default;


        public void Run(EcsSystems systems)
        {
            foreach (var i in _eventFilter)
            {
                ref var onBallDealtDamage = ref _eventFilter.Pools.Inc1.Get(i);
                if (!_targetFilter.Contains(onBallDealtDamage.To, out var targetIdx)) continue;
                if (!_ballFilter.Contains(onBallDealtDamage.Ball, out var ballIdx)) continue;

                var damageDealer = _ballFilter.Pools.Inc1.Get(ballIdx).Owner;
                if (!_damageDealerFilter.Contains(damageDealer, out var damageDealerIdx)) continue;

                ref readonly var burningParams = ref GetBurningParams(damageDealerIdx).Params;
                ref var burning = ref GetOrAdd<Burning>(targetIdx);
                burning.Params = burningParams;
                burning.ElapsedTime = 0f;
                burning.RemainingTimes = burningParams.DamageTimes;
                GetOrAdd<OnStartedBurning>(targetIdx);
            }
        }

        private ref BurningAttack GetBurningParams(int idx) => ref _damageDealerFilter.Pools.Inc1.Get(idx);
    }
}