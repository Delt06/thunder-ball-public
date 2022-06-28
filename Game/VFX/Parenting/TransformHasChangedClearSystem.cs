using DELTation.LeoEcsExtensions.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.VFX.Parenting
{
    public class TransformHasChangedClearSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnityRef<Transform>>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref readonly var transform = ref _filter.Pools.Inc1.Get(i).Object;
                transform.hasChanged = false;
            }
        }
    }
}