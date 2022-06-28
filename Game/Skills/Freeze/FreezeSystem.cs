using DELTation.LeoEcsExtensions.Utilities;
using Game.Stun;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Skills.Freeze
{
    public class FreezeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<IsFrozen>> _frozenFilter = default;
        private readonly EcsFilterInject<Inc<IsFrozen>, Exc<IsStunnedNow>> _frozenNotStunnedFilter = default;
        private readonly EcsPoolInject<IsStunnedNow> _isStunnedPool = default;
        private readonly EcsPoolInject<OnUnfroze> _onUnfrozePool = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _frozenNotStunnedFilter)
            {
                _isStunnedPool.Value.Add(i);
            }

            foreach (var i in _frozenFilter)
            {
                ref var isFrozen = ref _frozenFilter.Pools.Inc1.Get(i);
                isFrozen.RemainingTime -= Time.deltaTime;
                if (isFrozen.RemainingTime > 0) continue;

                _onUnfrozePool.Value.GetOrAdd(i);
                _frozenFilter.Pools.Inc1.Del(i);
            }
        }
    }
}