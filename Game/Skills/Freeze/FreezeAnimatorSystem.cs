using Game.Animations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Skills.Freeze
{
    public class FreezeAnimatorSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Animated, IsFrozen, OnFroze>> _frozeFilter = default;
        private readonly EcsFilterInject<Inc<Animated, OnUnfroze>, Exc<IsFrozen>> _unfrozeFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _frozeFilter)
            {
                var animator = _frozeFilter.Pools.Inc1.Get(i).Animator;
                animator.enabled = false;
            }

            foreach (var i in _unfrozeFilter)
            {
                var animator = _unfrozeFilter.Pools.Inc1.Get(i).Animator;
                animator.enabled = true;
            }
        }
    }
}