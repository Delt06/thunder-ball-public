using Game.Ball;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Cameras.Shake
{
    public class BallDamageCameraShakeSystem : IEcsRunSystem
    {
        private readonly CameraShake _cameraShake;
        private readonly EcsFilterInject<Inc<OnBallDealtDamage>> _filter = default;
        private readonly CameraShakePresetsConfig _presetsConfig;

        public BallDamageCameraShakeSystem(CameraShakePresetsConfig presetsConfig, CameraShake cameraShake)
        {
            _presetsConfig = presetsConfig;
            _cameraShake = cameraShake;
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var damage = _filter.Pools.Inc1.Get(i).Damage;
                var preset = _presetsConfig.DamageBase;
                preset.Amplitude *= _presetsConfig.AmplitudeOverDamage.Evaluate(damage);
                _cameraShake.Shake(preset);
            }
        }
    }
}