using Game.Obstacles;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.VFX
{
    public class ObstacleDestructionVfxSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnObstacleDestroyed>> _filter = default;
        private readonly VisualEffectFactory _visualEffectFactory;

        public ObstacleDestructionVfxSystem(VisualEffectFactory visualEffectFactory) =>
            _visualEffectFactory = visualEffectFactory;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var contactPoint = _filter.Pools.Inc1.Get(i).At;
                _visualEffectFactory.CreatePooledAndThenReturn(contactPoint, ve => ve.ObstacleDestruction);
            }
        }
    }
}