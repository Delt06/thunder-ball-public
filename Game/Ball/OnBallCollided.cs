using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Ball
{
    public struct OnBallCollided
    {
        public EcsPackedEntityWithWorld Other;
        public EcsPackedEntityWithWorld Ball;
        public Vector3 ContactPoint;
    }
}