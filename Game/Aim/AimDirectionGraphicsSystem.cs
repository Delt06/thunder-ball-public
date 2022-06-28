using Game.Ball;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Aim
{
    public class AimDirectionGraphicsSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AimDirectionGraphics, Player.Player, ActiveBallRef>> _activeFilter =
            default;
        private readonly EcsFilterInject<Inc<AimDirectionGraphics, Player.Player>, Exc<ActiveBallRef>> _inactiveFilter =
            default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _activeFilter)
            {
                var model = _activeFilter.Pools.Inc1.Get(i).Model;
                model.SetActive(false);
            }

            foreach (var i in _inactiveFilter)
            {
                var aimDirectionGraphics = _activeFilter.Pools.Inc1.Get(i);
                aimDirectionGraphics.Model.SetActive(true);
                aimDirectionGraphics.Transform.forward = _activeFilter.Pools.Inc2.Get(i).LastAimDirection;
            }
        }
    }
}