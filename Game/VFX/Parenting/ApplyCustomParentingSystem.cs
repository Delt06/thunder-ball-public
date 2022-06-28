using DELTation.LeoEcsExtensions.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.VFX.Parenting
{
    public class ApplyCustomParentingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CustomParenting, UnityRef<Transform>>, Exc<OnParentDestroyed>> _filter =
            default;
        private readonly EcsPoolInject<OnParentDestroyed> _onParentDestroyedPool = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var customParenting = ref _filter.Pools.Inc1.Get(i);
                var parent = customParenting.Parent;

                if (parent == null)
                {
                    _filter.Pools.Inc1.Del(i);
                    _onParentDestroyedPool.Value.Add(i);
                    continue;
                }

                var shouldChange = !customParenting.IsValid || parent.hasChanged;
                if (!shouldChange) continue;

                var transform = _filter.Pools.Inc2.Get(i).Object;
                var position = parent.TransformPoint(customParenting.LocalPosition);
                var rotation = parent.rotation * customParenting.LocalRotation;
                transform.SetPositionAndRotation(position, rotation);
                customParenting.IsValid = true;
            }
        }
    }
}