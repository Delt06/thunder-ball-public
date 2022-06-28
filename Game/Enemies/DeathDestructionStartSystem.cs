using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using Game.Destruction;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Enemies
{
    public class DeathDestructionStartSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HealthData, UpdateEvent<HealthData>>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var destructibleTarget = ref _filter.Pools.Inc1.Get(i);
                if (destructibleTarget.Health > 0f) continue;
                Add<EntityDestructionCommand>(i);
            }
        }
    }
}