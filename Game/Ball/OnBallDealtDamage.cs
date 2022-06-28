using Leopotam.EcsLite;

namespace Game.Ball
{
    public struct OnBallDealtDamage
    {
        public float Damage;
        public EcsPackedEntityWithWorld Ball;
        public EcsPackedEntityWithWorld To;
    }
}