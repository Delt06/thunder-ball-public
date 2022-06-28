using Game._Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Loot.Assets
{
    public abstract class LootAssetBase : ScriptableObject
    {
        protected const string AssetPath = "Loot/";
        public abstract void CreateAt(ILootPrefabs lootPrefabs, Vector3 position);

        protected static TPrefab CreateAt<TPrefab>(TPrefab prefab, Vector3 at) where TPrefab : Object =>
            Instantiate(prefab, at, Quaternion.identity);

        public abstract IEcsSystem CreateLootSystem();

        public virtual bool TryCreateStatPresentationSystem(UiSceneData ui, out IEcsSystem system)
        {
            system = default;
            return false;
        }
    }
}