using Game.VFX.Parenting;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.VFX.Pools
{
    public class OnParentDestroyedReturnSimpleVfxToPool : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SimpleVfxHandle, OnParentDestroyed>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref readonly var simpleVfxHandle = ref _filter.Pools.Inc1.Get(i);
                simpleVfxHandle.ReturnToPool();
            }
        }
    }
}