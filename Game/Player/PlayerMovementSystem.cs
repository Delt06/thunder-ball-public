using DELTation.LeoEcsExtensions.ExtendedPools;
using Game._Data;
using Game.Ball;
using Game.Player.Controls;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Player, ActiveBallRef>> _filter = default;
        private readonly EcsSingletonPool<PlayerInputData> _playerInputDatas;
        private readonly StaticData _staticData;

        public PlayerMovementSystem(EcsWorld world, StaticData staticData)
        {
            _staticData = staticData;
            _playerInputDatas = world.GetSingletonPool<PlayerInputData>();
        }

        public void Run(EcsSystems systems)
        {
            ref var playerInputData = ref _playerInputDatas.Get();
            ref var directionX = ref playerInputData.DirectionUnclamped.x;
            if (Mathf.Approximately(directionX, 0f)) return;
            ref var pressPlayerPosition = ref playerInputData.PressPlayerPosition;

            foreach (var i in _filter)
            {
                ref var player = ref _filter.Pools.Inc1.Get(i);
                var playerTransform = player.Transform;

                var position = playerTransform.position;
                var newPosition = position;
                var targetX = pressPlayerPosition + directionX * player.MovementSensitivity;
                var xLimit = _staticData.XLimit;
                targetX = Mathf.Clamp(targetX, -xLimit, xLimit);
                var maxDistance = player.MovementSpeed * Time.deltaTime;
                newPosition.x = Mathf.MoveTowards(newPosition.x, targetX, maxDistance);
                player.LastMovementDirection = Mathf.Approximately(newPosition.x, position.x) ? 0f : directionX;
                playerTransform.position = newPosition;
            }
        }
    }
}