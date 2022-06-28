using System.Collections.Generic;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Game.VFX.Parenting;
using Game.VFX.Pools;
using Leopotam.EcsLite;
using UnityEngine;
using Utils;

namespace Game.VFX
{
    public class VisualEffectFactory
    {
        private const int VfxPoolCapacity = 8;
        private readonly Dictionary<ParticleSystem, VfxPool> _pools = new();
        private readonly Dictionary<GameObject, SimpleVfxPool> _simplePools = new();
        private readonly VisualEffects _visualEffects;
        private readonly EcsWorld _world;

        public VisualEffectFactory(VisualEffects visualEffects, EcsWorld world)
        {
            _visualEffects = visualEffects;
            _world = world;
        }

        public ParticleSystem CreateAndThenDestroy(Vector3 at, VisualEffectSelector selector, Transform parent = null)
        {
            var prefab = selector(_visualEffects);
            var prefabTransform = prefab.transform;
            var effect = Object.Instantiate(prefab, at + prefabTransform.localPosition, prefabTransform.localRotation,
                parent
            );
            if (!effect.isPlaying)
                effect.Play();
            var main = effect.main;
            main.stopAction = ParticleSystemStopAction.Destroy;
            return effect;
        }

        public EcsEntityWithData<VfxHandle> CreatePooledAndThenReturn(Vector3 at, VisualEffectSelector selector)
        {
            var prefab = selector(_visualEffects);
            var pool = GetOrCreateVfxPool(prefab);
            var prefabTransform = prefab.transform;
            var vfxEntity = pool.Create(at + prefabTransform.localPosition, prefabTransform.localRotation);
            var vfx = vfxEntity.Get().Vfx;
            if (!vfx.isPlaying)
                vfx.Play();
            return vfxEntity;
        }

        private VfxPool GetOrCreateVfxPool(ParticleSystem prefab)
        {
            if (_pools.TryGetValue(prefab, out var pool))
                return pool;

            pool = new GameObject("VfxPool_" + prefab.name).AddComponent<VfxPool>();
            pool.Init(_world, prefab, VfxPoolCapacity);
            _pools[prefab] = pool;
            return pool;
        }

        private SimpleVfxPool GetOrCreateSimpleVfxPool(GameObject prefab)
        {
            if (_simplePools.TryGetValue(prefab, out var pool))
                return pool;

            pool = new GameObject("VfxPool_Simple_" + prefab.name).AddComponent<SimpleVfxPool>();
            pool.Init(_world, prefab, VfxPoolCapacity);
            _simplePools[prefab] = pool;
            return pool;
        }

        public EcsEntityWithData<SimpleVfxHandle> CreateAndThenDestroy(Vector3 at, SimpleVisualEffectSelector selector,
            Transform parent = null)
        {
            var prefab = selector(_visualEffects);
            var vfxPool = GetOrCreateSimpleVfxPool(prefab);
            var prefabTransform = prefab.transform;
            var position = at + prefabTransform.localPosition;
            var rotation = prefabTransform.localRotation;
            var entityWithData = vfxPool.Create(position, rotation);
            if (parent != null)
            {
                var transform = entityWithData.Get().Vfx.transform;
                entityWithData.Entity.Add<UnityRef<Transform>>().Object = transform;
                ref var customParenting = ref entityWithData.Entity.Add<CustomParenting>();
                customParenting.Parent = parent;
                customParenting.LocalPosition = parent.InverseTransformPoint(position);
                customParenting.LocalRotation = Quaternion.Inverse(parent.rotation) * rotation;
            }

            return entityWithData;
        }
    }
}