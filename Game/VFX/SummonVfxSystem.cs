using DELTation.LeoEcsExtensions.Components;
using Game.Enemies.Specific;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.VFX
{
    public class SummonVfxSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnSummoned>> _filter = default;
        private readonly EcsFilterInject<Inc<UnityRef<Transform>>> _summonedFilter = default;
        private readonly VisualEffectFactory _visualEffectFactory;

        public SummonVfxSystem(VisualEffectFactory visualEffectFactory) => _visualEffectFactory = visualEffectFactory;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var summonedEntity = _filter.Pools.Inc1.Get(i).SummonedEntity;
                if (!_summonedFilter.Contains(summonedEntity, out var summonedEntityIdx)) continue;

                var transform = _summonedFilter.Pools.Inc1.Get(summonedEntityIdx).Object;
                _visualEffectFactory.CreatePooledAndThenReturn(transform.position, ve => ve.Summon);
            }
        }
    }
}