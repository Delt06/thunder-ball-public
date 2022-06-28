using System;
using Game.Loot.Assets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Loot.SmartDrop
{
    [CreateAssetMenu]
    public class SmartDropConfig : ScriptableObject
    {
        [SerializeField] [TableList] private LootDrop[] _lootDrops;
        [SerializeField] [Min(0f)] private float _totalSkillsDropWeight = 1f;

        public LootDrop[] LootDrops => _lootDrops;

        public float TotalSkillsDropWeight => _totalSkillsDropWeight;


        [Serializable]
        public struct LootDrop
        {
            [Required] [AssetSelector]
            public LootAssetBase Loot;
            [Min(0f)]
            public float Weight;
        }
    }
}