using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyTriggerPresenter : MonoBehaviour, IEntityInitializer
    {
        private EcsPackedEntityWithWorld _entity;

        private void OnTriggerEnter(Collider other)
        {
            if (!_entity.Unpack(out var world, out _)) return;

            var otherEntityView = other.GetComponentInParent<EntityView>();
            if (otherEntityView == null) return;
            if (!otherEntityView.TryGetEntity(out var otherEntity)) return;

            ref var onEnemyContact = ref world.NewEntityWith<OnEnemyContact>();
            onEnemyContact.Enemy = _entity;
            onEnemyContact.Other = otherEntity;
        }

        public void InitializeEntity(EcsPackedEntityWithWorld entity)
        {
            _entity = entity;
        }
    }
}