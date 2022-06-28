using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using Game.Enemies.Models;
using Game.Skills.Freeze;
using Game.VFX.Pools;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.VFX.Freezing
{
    public class FreezingVfxSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly VisualEffectFactory _factory;
        private readonly EcsFilterInject<Inc<UnityRef<Transform>, ModelSlot, OnFroze>, Exc<FreezingAttackEffect>>
            _frozeFilter = default;
        private readonly EcsFilterInject<Inc<FreezingAttackEffect, OnUnfroze>> _unfrozeFilter = default;

        public FreezingVfxSystem(VisualEffectFactory factory) => _factory = factory;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _frozeFilter)
            {
                var at = _frozeFilter.Pools.Inc1.Get(i).Object.position;
                var parent = _frozeFilter.Pools.Inc2.Get(i).Model.transform;
                var effect = _factory.CreateAndThenDestroy(at, ve => ve.IceCube, parent);
                Add<FreezingAttackEffect>(i).Effect = effect;
            }

            foreach (var i in _unfrozeFilter)
            {
                var effect = _unfrozeFilter.Pools.Inc1.Get(i);
                effect.Effect.Get().ReturnToPool();
                _unfrozeFilter.Pools.Inc1.Del(i);
            }
        }
    }
}