using DELTation.LeoEcsExtensions.Composition.Di;
using Game.VFX.Burning;
using Game.VFX.Crit;
using Game.VFX.Freezing;

namespace Game.VFX
{
    public class VfxFeature : PrebuiltFeature
    {
        protected override void ConfigureBuilder(EcsFeatureBuilder featureBuilder)
        {
            featureBuilder
                .CreateAndAdd<BallImpactVfxSystem>()
                .CreateAndAdd<BallDirectedVfxSystem>()
                .CreateAndAdd<ObstacleDestructionVfxSystem>()
                .CreateAndAdd<TeleportVfxSystem>()
                .CreateAndAdd<SummonVfxSystem>()
                .CreateAndAdd<BurningVfxSystem>()
                .CreateAndAdd<FreezingVfxSystem>()
                .CreateAndAdd<CritVfxSystem>()
                ;
        }
    }
}