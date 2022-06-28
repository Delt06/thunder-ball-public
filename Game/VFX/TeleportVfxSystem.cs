using DELTation.LeoEcsExtensions.Components;
using Game.Enemies.Specific;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.VFX
{
    public class TeleportVfxSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnTeleported, UnityRef<Transform>>> _filter = default;
        private readonly VisualEffectFactory _visualEffectFactory;

        public TeleportVfxSystem(VisualEffectFactory visualEffectFactory) =>
            _visualEffectFactory = visualEffectFactory;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var from = _filter.Pools.Inc1.Get(i).From;
                var to = _filter.Pools.Inc2.Get(i).Object.position;
                _visualEffectFactory.CreatePooledAndThenReturn(from, ve => ve.DevilTeleport);
                _visualEffectFactory.CreatePooledAndThenReturn(to, ve => ve.DevilTeleport);
            }
        }
    }
}