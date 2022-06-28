using DELTation.LeoEcsExtensions.Views;
using Game.Attributes;
using Game.Enemies.Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemies
{
    [CreateAssetMenu]
    public class EnemyPreset : ScriptableObject
    {
        [SerializeField] [Required] [AssetSelector(Paths = "Assets/Core/Characters/EntityViews")]
        private EntityView _entityViewPrefab;
        [SerializeField] [Required] [PrefabSelector]
        private EnemyModel _modelPrefab;

        public EntityView EntityViewPrefab => _entityViewPrefab;

        public EnemyModel ModelPrefab => _modelPrefab;
    }
}