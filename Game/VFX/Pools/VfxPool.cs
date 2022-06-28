using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;
using Utils;

namespace Game.VFX.Pools
{
    public class VfxPool : MonoBehaviour
    {
        private readonly List<PooledVfx> _all = new();
        private readonly HashSet<int> _free = new();
        private readonly Dictionary<ParticleSystem, int> _indices = new();
        private ParticleSystem _prefab;
        private EcsWorld _world;

        public void Init(EcsWorld world, ParticleSystem prefab, int capacity)
        {
            _world = world;
            _prefab = prefab;
            CreateFreeObjects(capacity);
        }

        private void CreateFreeObjects(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var vfx = Instantiate(_prefab, transform);
                var mainModule = vfx.main;
                mainModule.stopAction = ParticleSystemStopAction.Callback;

                var vfxPoolBehavior = vfx.gameObject.AddComponent<VfxPoolBehavior>();
                var index = _all.Count;
                _all.Add(new PooledVfx
                    {
                        VfxPoolBehavior = vfxPoolBehavior,
                        Vfx = vfx,
                        Index = index,
                    }
                );
                _free.Add(index);
                _indices[vfx] = index;
                Deactivate(vfx);
            }
        }


        public EcsEntityWithData<VfxHandle> Create(Vector3 position, Quaternion rotation)
        {
            if (_free.Count == 0)
                CreateFreeObjects(_all.Count);

            if (!TryGetFirstFree(out var pooledVfx))
                throw new InvalidOperationException("No free vfx objects.");

            _free.Remove(pooledVfx.Index);
            Activate(pooledVfx.Vfx);
            var entity = new EcsEntityWithData<VfxHandle>(_world, new VfxHandle
                {
                    Vfx = pooledVfx.Vfx,
                }
            );
            pooledVfx.VfxPoolBehavior.Init(this, entity);
            pooledVfx.Vfx.transform.SetPositionAndRotation(position, rotation);
            return entity;
        }

        private bool TryGetFirstFree(out PooledVfx vfx)
        {
            foreach (var vfxIndex in _free)
            {
                vfx = _all[vfxIndex];
                return true;
            }

            vfx = default;
            return false;
        }

        public void Return(EcsEntityWithData<VfxHandle> entity)
        {
            var vfx = entity.Get().Vfx;
            if (!_indices.TryGetValue(vfx, out var index))
                throw new InvalidOperationException("VFX is not in the pool.");
            Deactivate(vfx);
            _free.Add(index);
            entity.Destroy();
        }

        private static void Deactivate(ParticleSystem vfx)
        {
            StopAndClear(vfx);
            vfx.gameObject.SetActive(false);
        }

        private static void Activate(ParticleSystem vfx)
        {
            StopAndClear(vfx);
            vfx.gameObject.SetActive(true);
        }

        private static void StopAndClear(ParticleSystem vfx)
        {
            vfx.Stop();
            vfx.Clear();
        }

        private struct PooledVfx : IEquatable<PooledVfx>
        {
            public ParticleSystem Vfx;
            public VfxPoolBehavior VfxPoolBehavior;
            public int Index;

            public bool Equals(PooledVfx other) => Index == other.Index;

            public override bool Equals(object obj) => obj is PooledVfx other && Equals(other);

            public override int GetHashCode() => Index;
        }
    }
}