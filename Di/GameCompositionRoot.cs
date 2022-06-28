using Cheats;
using DELTation.DIFramework;
using DELTation.DIFramework.Cheats;
using DELTation.DIFramework.Containers;
using DELTation.LeoEcsExtensions.Composition.Di;
using Game._Data;
using Game.Cameras.Shake;
using Game.Enemies;
using Game.Levels;
using Game.Loot.HealthIncrease;
using Game.Loot.SmartDrop;
using Game.VFX;
using Game.Waves.Difficulty;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Di
{
    public class GameCompositionRoot : DependencyContainerBase
    {
        [SerializeField] [Required] private SceneData _sceneData;
        [SerializeField] [Required] private StaticData _staticData;
        [SerializeField] [Required] private UiSceneData _uiSceneData;

        protected override void ComposeDependencies(ICanRegisterContainerBuilder builder)
        {
            builder
                .RegisterEcsEntryPoint<GameEcsEntryPoint>()
                .AttachEcsEntryPointViewTo(gameObject)
                ;

            builder.RegisterCheatMenu<GameCheats>();

            builder
                .Register(_sceneData)
                .Register(_staticData)
                .RegisterFromMethod((StaticData staticData) => staticData.LootPrefabs)
                .RegisterFromMethod((StaticData staticData) => staticData.VisualEffects)
                .RegisterFromMethod((StaticData staticData) => staticData.CameraShakePresetsConfig)
                .RegisterFromMethod((StaticData staticData) => staticData.SmartDropConfig)
                .RegisterFromMethod((StaticData staticData) => staticData.ProgressionConfig)
                .Register(_uiSceneData)
                .RegisterFromMethod((UiSceneData uiSceneData) => uiSceneData.ScreenManager)
                ;

            builder
                .Register<LevelLoader>()
                .Register<VisualEffectFactory>()
                .Register<EnemyFactory>()
                .Register<LootAssetDatabase>()
                .Register<CameraShake>()
                .Register<SmartDropService>()
                .Register<DifficultyService>()
                ;
        }
    }
}