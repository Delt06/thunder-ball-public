using DELTation.LeoEcsExtensions.Composition.Di;
using Game.Ui;

namespace Di
{
    public class ScreensFeature : PrebuiltFeature
    {
        protected override void ConfigureBuilder(EcsFeatureBuilder featureBuilder)
        {
            featureBuilder
                .CreateAndAdd<ScreenManagerInitSystem>()
                .CreateAndAdd<WinLoseScreenMediationSystem>()
                ;
        }
    }
}