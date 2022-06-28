using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using Game.Enemies.Models;
using Game.Skills.Burning;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.VFX.Burning
{
    public class BurningVfxSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly VisualEffectFactory _factory;
        private readonly EcsFilterInject<Inc<UnityRef<Transform>, ModelSlot, OnStartedBurning>, Exc<BurningVfx>>
            _startFilter = default;
        private readonly EcsFilterInject<Inc<BurningVfx, OnStoppedBurning>> _stopFilter = default;

        public BurningVfxSystem(VisualEffectFactory factory) => _factory = factory;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _startFilter)
            {
                var at = _startFilter.Pools.Inc1.Get(i).Object.position;
                var parent = _startFilter.Pools.Inc2.Get(i).Model.transform;
                var particleSystem = _factory.CreateAndThenDestroy(at, ve => ve.Burning, parent);
                Add<BurningVfx>(i).ParticleSystem = particleSystem;
            }

            foreach (var i in _stopFilter)
            {
                var burningVfx = _stopFilter.Pools.Inc1.Get(i);
                var particleSystem = burningVfx.ParticleSystem;
                particleSystem.Stop();
                _stopFilter.Pools.Inc1.Del(i);
            }
        }
    }
}