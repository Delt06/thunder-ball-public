using System;
using DELTation.LeoEcsExtensions.Systems.Run;
using Game._Data;
using Game.Ball;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Ui.BallReturn
{
    public class BallReturnPresentationSystem : EcsSystemBase, IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsFilterInject<Inc<Ball.Ball>> _ballFilter = default;
        private readonly BallReturnButton _ballReturnButton;
        private readonly StaticData _staticData;
        private ButtonState? _buttonState;
        private float _timeWithBall;


        public BallReturnPresentationSystem(UiSceneData ui, StaticData staticData)
        {
            _staticData = staticData;
            _ballReturnButton = ui.BallReturnButton;
        }

        public void Destroy(EcsSystems systems)
        {
            _ballReturnButton.OnClick.RemoveListener(OnClicked);
        }

        public void Init(EcsSystems systems)
        {
            _ballReturnButton.OnClick.AddListener(OnClicked);
        }

        public void Run(EcsSystems systems)
        {
            if (_ballFilter.IsEmpty())
                _timeWithBall = 0f;
            else
                _timeWithBall += Time.deltaTime;

            var desiredButtonState = GetDesiredButtonState();
            if (_buttonState == desiredButtonState) return;

            _buttonState = desiredButtonState;
            switch (desiredButtonState)
            {
                case ButtonState.Shown:
                    _ballReturnButton.Show();
                    break;
                case ButtonState.Hidden:
                    _ballReturnButton.Hide();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ButtonState GetDesiredButtonState() =>
            _timeWithBall <= _staticData.TimeToShowBallReturnButton ? ButtonState.Hidden : ButtonState.Shown;

        private void OnClicked()
        {
            foreach (var i in _ballFilter)
            {
                GetOrAdd<DestroyBallCommand>(i);
            }
        }

        private enum ButtonState
        {
            Shown,
            Hidden,
        }
    }
}