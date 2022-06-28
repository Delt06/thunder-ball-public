using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views.Components;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;

namespace Game.Enemies
{
    public class HealthDataView : ComponentView<HealthData>
    {
        protected override void PostInitializeEntity(EcsPackedEntityWithWorld entity)
        {
            base.PostInitializeEntity(entity);
            ref var healthData = ref entity.Modify<HealthData>();
            healthData.MaxHealth = healthData.Health;
        }

        [HideInEditorMode]
        [Button]
        private void Kill() => Entity.Modify<HealthData>().Health = 0;
    }
}