using DELTation.LeoEcsExtensions.Components;
using Game._Data;
using Game.Stun;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Enemies.Specific
{
    public class OscillatingMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
                Inc<EnemyMovement, OscillatingMovement, UnityRef<Transform>>, Exc<IsStunnedNow>>
            _filter = default;
        private readonly StaticData _staticData;

        public OscillatingMovementSystem(StaticData staticData) => _staticData = staticData;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var reachedDesiredDistance = _filter.Pools.Inc1.Get(i).ReachedDesiredDistance;
                if (reachedDesiredDistance) continue;

                ref var oscillatingMovement = ref _filter.Pools.Inc2.Get(i);
                var offset = oscillatingMovement.Curve.Evaluate(oscillatingMovement.Time * oscillatingMovement.Speed) *
                             oscillatingMovement.Amplitude;
                var x = oscillatingMovement.StartX + offset;
                var xLimit = _staticData.XLimit;
                x = Mathf.Clamp(x, -xLimit, xLimit);
                oscillatingMovement.Time += Time.deltaTime;

                var transform = _filter.Pools.Inc3.Get(i).Object;
                var transformPosition = transform.position;
                transformPosition.x = x;
                transform.position = transformPosition;
            }
        }
    }
}