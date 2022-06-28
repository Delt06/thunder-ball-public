using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game.Ball;
using Game.Enemies;
using Game.Loot;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Utils;

namespace Game.Skills.Crit
{
    public class CritAttackSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Ball.Ball, OwnerRef>> _ballFilter = default;
        private readonly EcsFilterInject<Inc<CritAttack>> _damageDealerFilter = default;
        private readonly EcsFilterInject<Inc<OnBallDealtDamage>> _onBallDealtDamageFilter = default;
        private readonly EcsFilterInject<Inc<HealthData, UnityRef<Transform>>> _targetFilter = default;


        public void Run(EcsSystems systems)
        {
            foreach (var i in _onBallDealtDamageFilter)
            {
                ref var onBallDealtDamage = ref _onBallDealtDamageFilter.Pools.Inc1.Get(i);
                if (!_ballFilter.Contains(onBallDealtDamage.Ball, out var ballIdx)) continue;

                var damageDealer = _ballFilter.Pools.Inc2.Get(ballIdx).Owner;
                if (!_damageDealerFilter.Contains(damageDealer, out var damageDealerIdx)) continue;

                ref readonly var critAttack = ref _damageDealerFilter.Pools.Inc1.Get(damageDealerIdx);
                if (!RandomUtils.TryProbability(critAttack.Params.Probability)) continue;

                var target = onBallDealtDamage.To;
                if (!_targetFilter.Contains(target, out var targetIdx)) continue;

                var extraDamage = onBallDealtDamage.Damage * critAttack.Params.ExtraMultiplier;
                ref var healthData = ref _targetFilter.Pools.Inc1.Modify(targetIdx);
                healthData.Health -= extraDamage;
                onBallDealtDamage.Damage += extraDamage;
                var transform = _targetFilter.Pools.Inc2.Get(targetIdx).Object;
                World.NewEntityWith<OnCritAttack>().At = transform.position;
            }
        }
    }
}