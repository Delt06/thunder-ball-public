using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Pool;
using Utils;

namespace Game.VFX.Pools
{
    public class SimpleVfxPool : MonoBehaviour
    {
        private ObjectPool<GameObject> _pool;
        private EcsWorld _world;

        public void Init(EcsWorld world, GameObject prefab, int capacity)
        {
            _world = world;
            _pool = new ObjectPool<GameObject>(() => Instantiate(prefab, transform),
                go => go.SetActive(true),
                go => go.SetActive(false),
                defaultCapacity: capacity
            );
        }

        public EcsEntityWithData<SimpleVfxHandle> Create(Vector3 position, Quaternion rotation)
        {
            var vfx = _pool.Get();
            vfx.transform.SetPositionAndRotation(position, rotation);

            var entity = new EcsEntityWithData<SimpleVfxHandle>(_world, new SimpleVfxHandle
                {
                    Vfx = vfx,
                    Pool = this,
                }
            );
            entity.Get().Entity = entity;
            return entity;
        }

        public void Return(EcsEntityWithData<SimpleVfxHandle> entity)
        {
            var vfx = entity.Get().Vfx;
            entity.Destroy();
            _pool.Release(vfx);
        }
    }
}