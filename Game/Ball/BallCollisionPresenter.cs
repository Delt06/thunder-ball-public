using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Ball
{
    public class BallCollisionPresenter : MonoBehaviour, IEntityInitializer
    {
        private EcsPackedEntityWithWorld _entity;

        private void OnCollisionEnter(Collision collision)
        {
            if (!_entity.Unpack(out var world, out _)) return;

            var otherView = collision.collider.GetComponentInParent<EntityView>();
            if (otherView == null) return;
            if (!otherView.TryGetEntity(out var otherEntity)) return;

            ref var onBallCollided = ref world.NewEntityWith<OnBallCollided>();
            onBallCollided.Ball = _entity;
            onBallCollided.Other = otherEntity;
            onBallCollided.ContactPoint = collision.GetContact(0).point;
        }

        public void InitializeEntity(EcsPackedEntityWithWorld entity)
        {
            _entity = entity;
        }
    }
}