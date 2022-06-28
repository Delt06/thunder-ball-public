using Game.Stun;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Enemies
{
    public class AttackCooldownSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackCooldown>, Exc<IsStunnedNow>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var attackCooldown = ref _filter.Pools.Inc1.Get(i);
                attackCooldown.RemainingTime -= Time.deltaTime;
                if (attackCooldown.RemainingTime > 0f) continue;

                _filter.Pools.Inc1.Del(i);
            }
        }
    }
}