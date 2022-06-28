using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Game.Player.Controls
{
    public class PlayerInputSetupSystem : EcsSystemBase, IEcsInitSystem
    {
        public void Init(EcsSystems systems)
        {
            World.NewEntityWith<PlayerInputData>();
        }
    }
}