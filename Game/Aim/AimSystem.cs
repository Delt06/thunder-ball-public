using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game._Data;
using Game.Ball;
using Game.Player.Controls;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Aim
{
    public class AimSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Player.Player, Combo.Combo, BaseBallDamage>, Exc<ActiveBallRef>> _filter =
            default;
        private readonly EcsSingletonPool<PlayerInputData> _playerInputDatas;

        private readonly SceneData _sceneData;
        private readonly StaticData _staticData;

        public AimSystem(SceneData sceneData, StaticData staticData, EcsWorld world)
        {
            _sceneData = sceneData;
            _staticData = staticData;
            _playerInputDatas = world.GetSingletonPool<PlayerInputData>();
        }


        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var playerInputData = _playerInputDatas.Get();
                var cameraTransform = _sceneData.Camera.transform;
                var direction2 = playerInputData.ReleaseDirection ?? playerInputData.Direction;
                var direction = NormalizedWithoutY(cameraTransform.forward) * direction2.y +
                                NormalizedWithoutY(cameraTransform.right) * direction2.x;
                direction.Normalize();

                ref var player = ref _filter.Pools.Inc1.Get(i);
                direction = LimitAngle(player.MaxAngle, direction);
                player.LastAimDirection = direction;

                if (playerInputData.ReleaseDirection == null) continue;

                var position = player.Transform.position;
                var ballSpawnPosition = position + _staticData.BallSpawnOffset + player.BallSpawnDistance * direction;
                var ballEntityView = Object.Instantiate(_staticData.BallPrefab, ballSpawnPosition, Quaternion.identity);
                var ballEntity = ballEntityView.GetOrCreateEntity();
                ref var ball = ref ballEntity.Get<Ball.Ball>();
                ball.Rigidbody.velocity = direction * ball.Speed;
                ball.Speed = player.BallSpeed;
                ballEntity.Add<OwnerRef>().Owner = World.PackEntityWithWorld(i);

                var combos = _filter.Pools.Inc2.Get(i).Count;
                var baseDamage = _filter.Pools.Inc3.Get(i).Damage;
                var comboFactor = _staticData.DamageOverCombos.Evaluate(combos);
                ballEntity.Add<BallDamage>().Damage = baseDamage * comboFactor;

                Add<ActiveBallRef>(i).BallEntity = ballEntity;
                Add<OnLaunchedBall>(i);
            }
        }

        private static Vector3 NormalizedWithoutY(Vector3 vector)
        {
            vector.y = 0f;
            return vector;
        }

        private static Vector3 LimitAngle(float angleLimit, Vector3 direction)
        {
            var referenceDirection = Vector3.forward;
            var angle = Vector3.SignedAngle(referenceDirection, direction, Vector3.up);
            angle = Mathf.Clamp(angle, -angleLimit, angleLimit);
            return Quaternion.Euler(0, angle, 0) * referenceDirection;
        }
    }
}