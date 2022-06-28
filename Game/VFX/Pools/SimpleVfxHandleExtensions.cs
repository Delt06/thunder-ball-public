namespace Game.VFX.Pools
{
    public static class SimpleVfxHandleExtensions
    {
        public static void ReturnToPool(this in SimpleVfxHandle handle) =>
            handle.Pool.Return(handle.Entity);
    }
}