using DELTation.LeoEcsExtensions.Systems.Run;
using Game.VFX;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Destruction
{
    public class DeadBodyLifetimeSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeadBody>> _filter = default;
        private readonly VisualEffectFactory _visualEffectFactory;

        public DeadBodyLifetimeSystem(VisualEffectFactory visualEffectFactory) =>
            _visualEffectFactory = visualEffectFactory;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var deadBody = ref _filter.Pools.Inc1.Get(i);
                if (!deadBody.HasLifetime) continue;

                deadBody.Lifetime -= Time.deltaTime;
                if (deadBody.Lifetime > 0f) continue;

                _visualEffectFactory.CreatePooledAndThenReturn(deadBody.GameObject.transform.position,
                    ve => ve.DeadBodyDestruction
                );
                Object.Destroy(deadBody.GameObject);
                Del<DeadBody>(i);
            }
        }
    }
}