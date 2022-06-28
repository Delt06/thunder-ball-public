using Game.Ball;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Utils;

namespace Game.Combo
{
    public class ComboSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Combo, OnBallCaught>> _caughtFilter = default;
        private readonly EcsFilterInject<Inc<Combo, OnBallDestroyed>> _destroyedFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _destroyedFilter)
            {
                _destroyedFilter.Pools.Inc1.Modify(i).Count = 0;
            }

            foreach (var i in _caughtFilter)
            {
                _caughtFilter.Pools.Inc1.Modify(i).Count++;
            }
        }
    }
}