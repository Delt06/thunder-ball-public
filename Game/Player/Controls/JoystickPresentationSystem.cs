using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game._Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Player.Controls
{
    public class JoystickPresentationSystem : EcsSystemBase, IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
    {
        private readonly Joystick _joystick;
        private readonly EcsFilterInject<Inc<OnJoystickPressed>> _onPressedFilter = default;
        private readonly EcsFilterInject<Inc<OnJoystickReleased>> _onReleasedFilter = default;
        private readonly EcsFilterInject<Inc<Player>> _playerFilter = default;
        private readonly EcsSingletonPool<PlayerInputData> _playerInputDatas;

        public JoystickPresentationSystem(UiSceneData uiSceneData, EcsWorld world)
        {
            _joystick = uiSceneData.Joystick;
            _playerInputDatas = world.GetSingletonPool<PlayerInputData>();
        }

        public void Destroy(EcsSystems systems)
        {
            _joystick.PointerUp -= OnReleased;
            _joystick.PointerDown -= OnPressed;
        }

        public void Init(EcsSystems systems)
        {
            _joystick.PointerUp += OnReleased;
            _joystick.PointerDown += OnPressed;
        }


        public void Run(EcsSystems systems)
        {
            ref var playerInputData = ref _playerInputDatas.Get();
            playerInputData.Direction = _joystick.Value;
            playerInputData.DirectionUnclamped = _joystick.ValueUnclamped;

            foreach (var _ in _onPressedFilter)
            {
                foreach (var iPlayer in _playerFilter)
                {
                    var playerPositionX = _playerFilter.Pools.Inc1.Get(iPlayer).Transform.position.x;
                    _playerInputDatas.Get().PressPlayerPosition = playerPositionX;
                    break;
                }
            }

            foreach (var i in _onReleasedFilter)
            {
                _playerInputDatas.Get().ReleaseDirection = _onReleasedFilter.Pools.Inc1.Get(i).Direction;
            }
        }


        private void OnPressed() => World.NewEntityWith<OnJoystickPressed>();

        private void OnReleased(Vector2 direction) => World.NewEntityWith<OnJoystickReleased>().Direction = direction;
    }
}