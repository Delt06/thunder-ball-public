using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game.Destruction;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Player
{
    public class PlayerDeathDetectionSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Player, EntityDestructionCommand>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var _ in _filter)
            {
                World.NewEntityWith<OnPlayerDied>();
                break;
            }
        }
    }
}