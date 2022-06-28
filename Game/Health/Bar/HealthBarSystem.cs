using DELTation.LeoEcsExtensions.Components;
using Game._Data;
using Game.Enemies;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Health.Bar
{
    public class HealthBarSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HealthData, HealthBar, UpdateEvent<HealthData>>>
            _changeFilter = default;
        private readonly EcsFilterInject<Inc<HealthBar>> _filter = default;
        private readonly SceneData _sceneData;

        public HealthBarSystem(SceneData sceneData) => _sceneData = sceneData;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var transform = _filter.Pools.Inc1.Get(i).View.Transform;
                transform.forward = _sceneData.Camera.transform.forward;
            }

            foreach (var i in _changeFilter)
            {
                ref var destructibleTarget = ref _changeFilter.Pools.Inc1.Get(i);
                var healthBarView = _changeFilter.Pools.Inc2.Get(i).View;
                healthBarView.SetFill(destructibleTarget.Health / destructibleTarget.MaxHealth);
            }
        }
    }
}