using DELTation.LeoEcsExtensions.Views;

namespace Game.Loot.Assets
{
    public interface ILootPrefabs
    {
        EntityView Healing { get; }
        EntityView Damage { get; }
        EntityView HealthIncrease { get; }
        EntityView BurningAttack { get; }
        EntityView FreezingAttack { get; }
        EntityView CritAttack { get; }
    }
}