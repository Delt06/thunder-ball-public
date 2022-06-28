using Game.Enemies;
using Game.Loot.Collection;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Utils;

namespace Game.Loot.Healing
{
    public class HealingLootSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HealthData>> _healthFilter = default;
        private readonly EcsFilterInject<Inc<HealingLoot, IsBeingCollected>> _lootFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _lootFilter)
            {
                var healingLoot = _lootFilter.Pools.Inc1.Get(i);
                var collector = _lootFilter.Pools.Inc2.Get(i).Collector;
                if (!_healthFilter.Contains(collector, out var collectorIdx)) continue;

                ref var healthData = ref _healthFilter.Pools.Inc1.Modify(collectorIdx);
                healthData.Health = Mathf.Min(healthData.Health + healingLoot.HealAmount, healthData.MaxHealth);
            }
        }
    }
}