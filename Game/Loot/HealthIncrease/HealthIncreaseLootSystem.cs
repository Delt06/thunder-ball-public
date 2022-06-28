using Game.Enemies;
using Game.Loot.Collection;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Utils;

namespace Game.Loot.HealthIncrease
{
    public class HealthIncreaseLootSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HealthData>> _healthFilter = default;
        private readonly EcsFilterInject<Inc<HealthIncreaseLoot, IsBeingCollected>> _lootFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _lootFilter)
            {
                var healthIncreaseLoot = _lootFilter.Pools.Inc1.Get(i);
                var collector = _lootFilter.Pools.Inc2.Get(i).Collector;
                if (!_healthFilter.Contains(collector, out var collectorIdx)) continue;

                ref var healthData = ref _healthFilter.Pools.Inc1.Modify(collectorIdx);
                var ratio = healthData.Health / healthData.MaxHealth;
                healthData.MaxHealth += healthIncreaseLoot.ExtraHealth;
                healthData.Health = ratio * healthData.MaxHealth;
            }
        }
    }
}