using DELTation.LeoEcsExtensions.ExtendedPools;
using Leopotam.EcsLite;

namespace Game.Player.Controls
{
    public class PlayerInputCleanupSystem : IEcsRunSystem
    {
        private readonly EcsSingletonPool<PlayerInputData> _playerInputDatas;

        public PlayerInputCleanupSystem(EcsWorld world) =>
            _playerInputDatas = world.GetSingletonPool<PlayerInputData>();

        public void Run(EcsSystems systems)
        {
            ref var playerInputData = ref _playerInputDatas.Get();
            playerInputData.Direction = default;
            playerInputData.DirectionUnclamped = default;
            playerInputData.ReleaseDirection = default;
        }
    }
}