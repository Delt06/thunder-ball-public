using UnityEngine;
using Utils;

namespace Game.VFX.Pools
{
    public struct SimpleVfxHandle
    {
        public GameObject Vfx;
        public SimpleVfxPool Pool;
        public EcsEntityWithData<SimpleVfxHandle> Entity;
    }
}