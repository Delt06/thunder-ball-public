using Leopotam.EcsLite;

namespace Game.Enemies
{
    public struct OnEnemyContact
    {
        public EcsPackedEntityWithWorld Enemy;
        public EcsPackedEntityWithWorld Other;
    }
}