using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game.Stun;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Utils;

namespace Game.Enemies
{
    public class AttackUpdateSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Attack, ActiveAttack>, Exc<IsStunnedNow>> _filter = default;
        private readonly EcsFilterInject<Inc<Player.Player, HealthData>> _targetFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var attack = ref _filter.Pools.Inc1.Get(i);
                ref var activeAttack = ref _filter.Pools.Inc2.Get(i);
                activeAttack.ElapsedTime += Time.deltaTime;
                if (activeAttack.DealtDamage) continue;

                var dealDamageTime = attack.NormalizedDealDamageTime * attack.Duration;
                if (activeAttack.ElapsedTime < dealDamageTime) continue;

                DealDamage(attack.Damage);
                activeAttack.DealtDamage = true;
            }
        }

        private void DealDamage(float damage)
        {
            foreach (var i in _targetFilter)
            {
                _targetFilter.Pools.Inc2.Modify(i).Health -= damage;
                World.NewEntityWith<OnAttacked>().Damage = damage;
            }
        }
    }
}