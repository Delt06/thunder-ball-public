using DELTation.LeoEcsExtensions.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Ball.DamagePosition
{
    public class BallDamagePositionSystem : IEcsRunSystem
    {
        private readonly EcsPoolInject<BallDamagePosition> _ballDamagePositionPool = default;
        private readonly EcsFilterInject<Inc<OnBallDealtDamage>, Exc<BallDamagePosition>> _filter = default;
        private readonly EcsFilterInject<Inc<UnityRef<Transform>>> _targetFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var onBallDealtDamage = ref _filter.Pools.Inc1.Get(i);
                if (!_targetFilter.Contains(onBallDealtDamage.To, out var targetIdx)) continue;

                var transform = _targetFilter.Pools.Inc1.Get(targetIdx).Object;
                _ballDamagePositionPool.Value.Add(i).Position = transform.position;
            }
        }
    }
}