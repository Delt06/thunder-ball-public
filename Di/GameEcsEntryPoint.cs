using DELTation.LeoEcsExtensions.Composition.Di;
using Game.Aim;
using Game.Animations;
using Game.Animations.Enemy;
using Game.Animations.Player;
using Game.Ball;
using Game.Ball.DamagePosition;
using Game.Cameras;
using Game.Cameras.Shake;
using Game.Combo;
using Game.Destruction;
using Game.Enemies;
using Game.Enemies.Specific;
using Game.Graphics;
using Game.Health.Bar;
using Game.Loot;
using Game.Loot.Animation;
using Game.Loot.Collection;
using Game.Loot.Drop;
using Game.Loot.Skills;
using Game.Obstacles;
using Game.Player;
using Game.Player.Controls;
using Game.Skills.Burning;
using Game.Skills.Crit;
using Game.Skills.Freeze;
using Game.Stun;
using Game.Ui.BallReturn;
using Game.Ui.NewSkill;
using Game.Ui.Stats;
using Game.VFX;
using Game.VFX.Parenting;
using Game.VFX.Pools;
using Game.Waves;
using Game.Waves.Difficulty;

namespace Di
{
    public class GameEcsEntryPoint : EcsEntryPoint
    {
        public override void PopulateSystems(EcsFeatureBuilder featureBuilder)
        {
            featureBuilder
                .CreateAndAdd<TargetFrameRateSystem>()
                ;

            featureBuilder
                .CreateAndAdd<WaveInitSystem>()
                .CreateAndAdd<WaveEndDetectionSystem>()
                .CreateAndAdd<WaveSpawnSystem>()
                .CreateAndAdd<PlayerDifficultySystem>()
                .CreateAndAdd<BarrelSpawnSystem>()
                ;

            featureBuilder
                .CreateAndAdd<PlayerInputSetupSystem>()
                .CreateAndAdd<JoystickPresentationSystem>()
                .OneFrame<OnJoystickPressed>()
                .OneFrame<OnJoystickReleased>()
                ;

            featureBuilder
                .CreateAndAdd<AimSystem>()
                .CreateAndAdd<BallHorizontalVelocityPreventionSystem>()
                ;

            featureBuilder
                .CreateAndAdd<PlayerLastMovementDirectionResetSystem>()
                .CreateAndAdd<PlayerMovementSystem>()
                ;

            featureBuilder
                .CreateAndAdd<BallSpeedCorrectionSystem>()
                .CreateAndAdd<BallEnemyDamageSystem>()
                .CreateAndAdd<BallCatchingSystem>()
                .CreateAndAdd<BallDestructionSystem>()
                .OneFrame<DestroyBallCommand>()
                .CreateAndAdd<ComboSystem>()
                .CreateAndAdd<PlayerBallEndSystem>()
                .OneFrame<OnBallDestroyed>()
                .OneFrame<OnBallCaught>()
                .CreateAndAdd<BallDamagePositionSystem>()
                ;

            featureBuilder
                .CreateAndAdd<CritAttackSystem>()
                ;

            featureBuilder
                .CreateAndAdd<BurningStartSystem>()
                .CreateAndAdd<BurningSystem>()
                ;

            featureBuilder
                .CreateAndAdd<FreezingStartSystem>()
                .CreateAndAdd<FreezeSystem>()
                ;

            featureBuilder
                .CreateAndAdd<EnemyObstacleDestructionSystem>()
                ;

            featureBuilder
                .CreateAndAdd<TeleportationSystem>()
                .CreateAndAdd<OscillatingMovementSystem>()
                .CreateAndAdd<SummonSystem>()
                ;

            featureBuilder
                .CreateAndAdd<EnemyMovementSystem>()
                .CreateAndAdd<AttackStartSystem>()
                .CreateAndAdd<AttackUpdateSystem>()
                .CreateAndAdd<AttackEndSystem>()
                .CreateAndAdd<AttackCooldownSystem>()
                ;

            featureBuilder
                .CreateAndAdd<LootCollectionStartSystem>()
                .InjectBuilder<LootSystemBuilder>()
                .CreateAndAdd<LootCollectionEndSystem>()
                .OneFrame<IsBeingCollected>()
                .CreateAndAdd<LootAnimationSystem>()
                ;

            featureBuilder
                .CreateAndAdd<DeathDestructionStartSystem>()
                .CreateAndAdd<PlayerDeathDetectionSystem>()
                .CreateAndAdd<ObstacleDestructionDetectionSystem>()
                .CreateAndAdd<DropOnDestructionSystem>()
                .CreateAndAdd<DeathAnimationSystem>()
                .CreateAndAdd<EntityViewDestructionSystem>()
                .OneFrame<EntityDestructionCommand>()
                ;

            featureBuilder
                .CreateAndAdd<PlayerMovementAnimationSystem>()
                .CreateAndAdd<PlayerLaunchBallAnimation>()
                .CreateAndAdd<EnemyMovementAnimationSystem>()
                .CreateAndAdd<EnemyAttackAnimationSystem>()
                .CreateAndAdd<FreezeAnimatorSystem>()
                ;

            featureBuilder
                .AddFeature<VfxFeature>()
                ;

            featureBuilder
                .CreateAndAdd<DeadBodyLifetimeSystem>()
                ;

            featureBuilder
                .CreateAndAdd<HealthBarSpawnSystem>()
                .CreateAndAdd<HealthBarSystem>()
                .CreateAndAdd<AimDirectionGraphicsSystem>()
                .CreateAndAdd<PlayerComboPresentationSystem>()
                .CreateAndAdd<WaveLabelSystem>()
                ;

            featureBuilder
                .CreateAndAdd<LookAtCameraSystem>()
                .CreateAndAdd<BallDamageCameraShakeSystem>()
                .CreateAndAdd<AttackCameraShakeSystem>()
                .CreateAndAdd<CameraShakeSystem>()
                .OneFrame<CameraShakeCommand>()
                ;

            featureBuilder
                .InjectBuilder<StatPresentationSystemBuilder>()
                .CreateAndAdd<NewSkillPresentationSystem>()
                .CreateAndAdd<BallReturnPresentationSystem>()
                .AddFeature<ScreensFeature>()
                ;

            featureBuilder
                .CreateAndAdd<PlayerInputCleanupSystem>()
                .OneFrame<OnPlayerDied>()
                .OneFrame<OnFinishedWaves>()
                .OneFrame<OnBallCollided>()
                .OneFrame<OnBallDealtDamage>()
                .OneFrame<OnEnemyContact>()
                .OneFrame<OnLaunchedBall>()
                .OneFrame<OnStartedAttack>()
                .OneFrame<OnObstacleDestroyed>()
                .OneFrame<OnTeleported>()
                .OneFrame<OnSpawnedWave>()
                .OneFrame<OnSummoned>()
                .OneFrame<OnStartedBurning>()
                .OneFrame<OnStoppedBurning>()
                .OneFrame<OnFroze>()
                .OneFrame<OnUnfroze>()
                .OneFrame<IsStunnedNow>()
                .OneFrame<OnCritAttack>()
                .OneFrame<OnAttacked>()
                .OneFrame<OnLootedSkill>()
                .OneFrame<BallDamagePosition>()
                .OneFrameUpdateEvents()
                ;
        }

        public override void PopulateLateSystems(EcsFeatureBuilder featureBuilder)
        {
            featureBuilder
                .CreateAndAdd<ApplyCustomParentingSystem>()
                .CreateAndAdd<OnParentDestroyedReturnSimpleVfxToPool>()
                .OneFrame<OnParentDestroyed>()
                ;
        }
    }
}