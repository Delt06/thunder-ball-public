using UnityEngine;
using Utils;

namespace Game.VFX.Pools
{
    public class VfxPoolBehavior : MonoBehaviour
    {
        private EcsEntityWithData<VfxHandle> _entity;
        private VfxPool _pool;

        private void OnParticleSystemStopped()
        {
            _pool.Return(_entity);
            _entity = default;
            _pool = default;
        }

        public void Init(VfxPool pool, EcsEntityWithData<VfxHandle> entity)
        {
            _entity = entity;
            _pool = pool;
        }
    }
}