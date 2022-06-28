using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using Game._Data;
using Game.Stun;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Enemies.Specific
{
    public class TeleportationSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Teleportation, UnityRef<Transform>, EnemyMovement>, Exc<IsStunnedNow>>
            _filter = default;
        private readonly StaticData _staticData;

        public TeleportationSystem(StaticData staticData) => _staticData = staticData;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var teleportation = ref _filter.Pools.Inc1.Get(i);
                teleportation.ElapsedTime += Time.deltaTime;
                if (teleportation.ElapsedTime < teleportation.Period) continue;

                teleportation.ElapsedTime -= teleportation.Period;
                var transform = _filter.Pools.Inc2.Get(i).Object;
                var originalPosition = transform.position;
                var newPosition = originalPosition;
                var direction = _filter.Pools.Inc3.Get(i).Direction;
                newPosition += direction * teleportation.Step;
                var xLimit = _staticData.XLimit;
                newPosition.x = Random.Range(-xLimit, xLimit);
                transform.position = newPosition;

                GetOrAdd<OnTeleported>(i).From = originalPosition;
            }
        }
    }
}