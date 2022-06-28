using System;
using DELTation.LeoEcsExtensions.Views;
using Game.Cameras.Shake;
using Game.Health.Bar;
using Game.Loot.Assets;
using Game.Loot.SmartDrop;
using Game.VFX;
using Game.Waves;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game._Data
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [SerializeField] [Required] private LootPrefabs _lootPrefabs;
        [SerializeField] [Required] private VisualEffects _visualEffects;
        [SerializeField] [Required] private CameraShakePresetsConfig _cameraShakePresetsConfig;
        [SerializeField] [Required] private SmartDropConfig _smartDropConfig;
        [SerializeField] [Required] private ProgressionConfig _progressionConfig;
        [SerializeField] [Required] private EntityView _ballPrefab;
        [SerializeField] [Required] private BarrelSpawnAnimation _barrelSpawnAnimationPrefab;
        [SerializeField] [Required] private EntityView _barrelPrefab;
        [SerializeField] [MinMaxSlider(0, 10, true)]
        private Vector2Int _barrelsCountRange = new(2, 3);
        [SerializeField] [Min(0f)] private float _xLimit = 3;
        [SerializeField] private Vector3 _ballSpawnOffset = Vector3.up;
        [SerializeField] private AnimationCurve _damageOverCombos;
        [SerializeField] private TargetFrameRate _targetFrameRate = _Data.TargetFrameRate._60;
        [SerializeField] [TableList] private HealthBarPrefabMapping[] _healthBarViewMappings;
        [SerializeField] [Required] private Quaternion _enemyDefaultRotation = Quaternion.identity;
        [SerializeField] [Min(0f)] private float _waveRespawnDelay = 1f;
        [SerializeField] [Min(0f)] private float _timeToShowBallReturnButton = 2f;

        public LootPrefabs LootPrefabs => _lootPrefabs;

        public VisualEffects VisualEffects => _visualEffects;

        public CameraShakePresetsConfig CameraShakePresetsConfig => _cameraShakePresetsConfig;

        public SmartDropConfig SmartDropConfig => _smartDropConfig;

        public ProgressionConfig ProgressionConfig => _progressionConfig;

        public EntityView BallPrefab => _ballPrefab;

        public EntityView BarrelPrefab => _barrelPrefab;

        public Vector2Int BarrelsCountRange => _barrelsCountRange;
        public BarrelSpawnAnimation BarrelSpawnAnimationPrefab => _barrelSpawnAnimationPrefab;

        public float XLimit => _xLimit;

        public Vector3 BallSpawnOffset => _ballSpawnOffset;

        public AnimationCurve DamageOverCombos => _damageOverCombos;

        public int TargetFrameRate => (int) _targetFrameRate;

        public HealthBarPrefabMapping[] HealthBarViewMappings => _healthBarViewMappings;

        public Quaternion EnemyDefaultRotation => _enemyDefaultRotation;

        public float WaveRespawnDelay => _waveRespawnDelay;

        public float TimeToShowBallReturnButton => _timeToShowBallReturnButton;

        [Serializable]
        public struct HealthBarPrefabMapping
        {
            public HealthBarPlaceholder.HealthBarType Type;
            [Required]
            public HealthBarView Prefab;
        }
    }
}