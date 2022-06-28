using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Enemies
{
    public class AttackEndSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Attack, ActiveAttack>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var attack = ref _filter.Pools.Inc1.Get(i);
                ref var activeAttack = ref _filter.Pools.Inc2.Get(i);
                if (activeAttack.ElapsedTime < attack.Duration) continue;

                _filter.Pools.Inc2.Del(i);
                GetOrAdd<AttackCooldown>(i).RemainingTime = attack.Cooldown;
            }
        }
    }
}