using Game.Ball;
using Game.Loot.Collection;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Utils;

namespace Game.Loot.Damage
{
    public class DamageLootSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BaseBallDamage>> _damageFilter = default;
        private readonly EcsFilterInject<Inc<DamageLoot, IsBeingCollected>> _lootFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _lootFilter)
            {
                var damageLoot = _lootFilter.Pools.Inc1.Get(i);
                var collector = _lootFilter.Pools.Inc2.Get(i).Collector;
                if (!_damageFilter.Contains(collector, out var collectorIdx)) continue;

                ref var baseBallDamage = ref _damageFilter.Pools.Inc1.Modify(collectorIdx);
                baseBallDamage.Damage += damageLoot.DamageIncrease;
            }
        }
    }
}