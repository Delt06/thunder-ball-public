using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Ball
{
    public class PlayerBallEndSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ActiveBallRef, OnBallCaught>> _caughtFilter = default;
        private readonly EcsFilterInject<Inc<ActiveBallRef, OnBallDestroyed>> _destroyedFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _destroyedFilter)
            {
                _destroyedFilter.Pools.Inc1.Del(i);
            }

            foreach (var i in _caughtFilter)
            {
                _caughtFilter.Pools.Inc1.Del(i);
            }
        }
    }
}