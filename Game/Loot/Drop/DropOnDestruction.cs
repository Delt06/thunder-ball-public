using System;
using Game.Loot.Assets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Loot.Drop
{
    [Serializable]
    public struct DropOnDestruction
    {
        [Range(0f, 1f)]
        public float SkipProbability;
        [EnumToggleButtons]
        public DropMode Mode;
        [TableList] [InfoBox("Null - no loot.")] [ShowIf(nameof(IsFixedMode))]
        public WeightedLoot[] WeightedLoots;

        private bool IsFixedMode => Mode == DropMode.Fixed;

        [Serializable]
        public struct WeightedLoot
        {
            [Min(0f)]
            public float Weight;
            [AssetSelector]
            public LootAssetBase LootAsset;
        }
    }

    public enum DropMode
    {
        Fixed,
        Smart,
    }
}