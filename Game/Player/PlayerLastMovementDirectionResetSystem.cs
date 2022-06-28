using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Player
{
    public class PlayerLastMovementDirectionResetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Player>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                _filter.Pools.Inc1.Get(i).LastMovementDirection = 0f;
            }
        }
    }
}