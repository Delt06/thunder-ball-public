using System;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Utils
{
    public readonly struct EcsEntityWithData<T> where T : struct
    {
        public readonly EcsPackedEntityWithWorld Entity;
        private readonly EcsPool<T> _pool;

        public EcsEntityWithData(EcsWorld world, T initialData)
        {
            var idx = world.NewEntity();
            _pool = world.GetPool<T>();
            _pool.Add(idx) = initialData;
            Entity = world.PackEntityWithWorld(idx);
        }

        public void Destroy() => Entity.Destroy();

        public ref T Get()
        {
            if (!Entity.Unpack(out _, out var idx))
                throw new ArgumentException("Entity is not alive.");
            return ref _pool.Get(idx);
        }
    }
}