using Game.Enemies;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Cameras.Shake
{
    public class AttackCameraShakeSystem : IEcsRunSystem
    {
        private readonly CameraShake _cameraShake;
        private readonly EcsFilterInject<Inc<OnAttacked>> _filter = default;
        private readonly CameraShakePresetsConfig _presetsConfig;

        public AttackCameraShakeSystem(CameraShake cameraShake, CameraShakePresetsConfig presetsConfig)
        {
            _cameraShake = cameraShake;
            _presetsConfig = presetsConfig;
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                _cameraShake.Shake(_presetsConfig.EnemyDamage);
            }
        }
    }
}