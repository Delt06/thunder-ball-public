using Cinemachine;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using DG.Tweening.Core.Easing;
using Game._Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Cameras.Shake
{
    public class CameraShakeSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CameraShakeCommand>> _commandsFilter = default;
        private readonly CinemachineBasicMultiChannelPerlin _noise;
        private readonly EcsFilterInject<Inc<ActiveCameraShake>> _shakesFilter = default;

        public CameraShakeSystem(SceneData sceneData)
        {
            var virtualCamera = sceneData.VirtualCamera;
            _noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }


        public void Run(EcsSystems systems)
        {
            foreach (var i in _commandsFilter)
            {
                ref var cameraShakeCommand = ref _commandsFilter.Pools.Inc1.Get(i);
                ref var activeCameraShake = ref World.NewEntityWith<ActiveCameraShake>();
                activeCameraShake.Preset = cameraShakeCommand.Preset;
            }

            var totalAmplitude = 0f;

            foreach (var i in _shakesFilter)
            {
                ref var activeCameraShake = ref _shakesFilter.Pools.Inc1.Get(i);
                var amplitude = GetAmplitude(activeCameraShake);
                totalAmplitude += amplitude;
                activeCameraShake.ElapsedTime += Time.deltaTime;
                if (activeCameraShake.ElapsedTime >= activeCameraShake.Preset.Duration)
                    _shakesFilter.Pools.Inc1.Del(i);
            }

            _noise.m_AmplitudeGain = totalAmplitude;
        }

        private static float GetAmplitude(in ActiveCameraShake activeCameraShake)
        {
            var ease = activeCameraShake.Preset.Ease;
            var elapsedTime = activeCameraShake.ElapsedTime;
            var duration = activeCameraShake.Preset.Duration;
            const float overshootOrAmplitude = 1f;
            const float period = 0f;
            return EaseManager.Evaluate(ease, null, elapsedTime, duration, overshootOrAmplitude, period) *
                   activeCameraShake.Preset.Amplitude;
        }
    }
}