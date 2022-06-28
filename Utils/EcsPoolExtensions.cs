using System;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Utilities;
using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Utils
{
    public static class EcsPoolExtensions
    {
        public static ref T Modify<T>([NotNull] this EcsPool<T> pool, int entity) where T : struct
        {
#if DEBUG
            if (pool == null) throw new ArgumentNullException(nameof(pool));
#endif

            ref var component = ref pool.Get(entity);
            pool.GetWorld().GetUpdatesPool<T>().GetOrAdd(entity);
            return ref component;
        }
    }
}