using Game._Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Cameras
{
    public class LookAtCameraSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<LookAtCamera>> _filter = default;
        private readonly SceneData _sceneData;

        public LookAtCameraSystem(SceneData sceneData) => _sceneData = sceneData;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var transform = _filter.Pools.Inc1.Get(i).Transform;
                transform.forward = _sceneData.Camera.transform.forward;
            }
        }
    }
}