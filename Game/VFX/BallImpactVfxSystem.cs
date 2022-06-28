using Game.Ball;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.VFX
{
    public class BallImpactVfxSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnBallCollided>> _filter = default;
        private readonly VisualEffectFactory _visualEffectFactory;

        public BallImpactVfxSystem(VisualEffectFactory visualEffectFactory) =>
            _visualEffectFactory = visualEffectFactory;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var contactPoint = _filter.Pools.Inc1.Get(i).ContactPoint;
                _visualEffectFactory.CreatePooledAndThenReturn(contactPoint, ve => ve.BallImpact);
            }
        }
    }
}