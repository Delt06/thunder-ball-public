using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Game.Cameras.Shake
{
    public class CameraShake
    {
        private readonly EcsWorld _world;

        public CameraShake(EcsWorld world) => _world = world;

        public void Shake(in CameraShakePreset preset)
        {
            _world.NewEntityWith<CameraShakeCommand>().Preset = preset;
        }
    }
}