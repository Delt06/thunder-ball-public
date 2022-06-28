using DELTation.LeoEcsExtensions.Utilities;
using Game._Data;
using Game.Animations;
using Game.Enemies.Models;
using Game.Waves.Difficulty;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyFactory
    {
        private readonly DifficultyService _difficultyService;
        private readonly StaticData _staticData;

        public EnemyFactory(StaticData staticData, DifficultyService difficultyService)
        {
            _staticData = staticData;
            _difficultyService = difficultyService;
        }

        public EcsPackedEntityWithWorld Create(EnemyPreset preset, Vector3 position)
        {
            var rotation = _staticData.EnemyDefaultRotation;
            var entityView = Object.Instantiate(preset.EntityViewPrefab, position, rotation);

            var entity = entityView.GetOrCreateEntity();
            ref var enemyModelSlot = ref entity.Get<ModelSlot>();
            var enemyModel = Object.Instantiate(preset.ModelPrefab, enemyModelSlot.Slot);
            enemyModelSlot.Model = enemyModel.gameObject;

            var animator = enemyModel.Animator;
            animator.applyRootMotion = false;
            entity.Get<Animated>().Animator = animator;

            _difficultyService.ApplyDifficultyToEnemyOnce(entity);

            return entity;
        }
    }
}