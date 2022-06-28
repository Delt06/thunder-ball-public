using System;
using DELTation.LeoEcsExtensions.Systems.Run;
using Game._Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Object = UnityEngine.Object;

namespace Game.Health.Bar
{
    public class HealthBarSpawnSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HealthBarPlaceholder>> _filter = default;
        private readonly StaticData _staticData;

        public HealthBarSpawnSystem(StaticData staticData) => _staticData = staticData;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var healthBarPlaceholder = ref _filter.Pools.Inc1.Get(i);
                var at = healthBarPlaceholder.Transform;
                var prefab = GetPrefab(healthBarPlaceholder.Type);
                var healthBarView = Object.Instantiate(prefab, at);
                Add<HealthBar>(i).View = healthBarView;
                Del<HealthBarPlaceholder>(i);
            }
        }

        private HealthBarView GetPrefab(HealthBarPlaceholder.HealthBarType type)
        {
            foreach (var mapping in _staticData.HealthBarViewMappings)
            {
                if (mapping.Type == type)
                    return mapping.Prefab;
            }

            throw new ArgumentException("No prefab found for type " + type);
        }
    }
}