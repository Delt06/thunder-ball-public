using Game.Skills.Crit;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.VFX.Crit
{
    public class CritVfxSystem : IEcsRunSystem
    {
        private readonly VisualEffectFactory _factory;
        private readonly EcsFilterInject<Inc<OnCritAttack>> _filter = default;

        public CritVfxSystem(VisualEffectFactory factory) => _factory = factory;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var at = _filter.Pools.Inc1.Get(i).At;
                _factory.CreateAndThenDestroy(at, ve => ve.Crit);
            }
        }
    }
}