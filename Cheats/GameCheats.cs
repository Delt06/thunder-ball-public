using DELTation.DIFramework.Cheats;
using DELTation.LeoEcsExtensions.ExtendedPools;
using Game.Enemies;
using Game.Player;
using Leopotam.EcsLite;

namespace Cheats
{
    public class GameCheats : CheatMenuBase
    {
        protected override void Build()
        {
            CreateButton("Kill Enemies", () =>
                {
                    var world = GetService<EcsWorld>();
                    var filter = world.Filter<Enemy>().Inc<HealthData>().End();
                    KillAll(world, filter);
                }
            );

            CreateButton("Kill Player", () =>
                {
                    var world = GetService<EcsWorld>();
                    var filter = world.Filter<Player>().Inc<HealthData>().End();
                    KillAll(world, filter);
                }
            );
        }

        private static void KillAll(EcsWorld world, EcsFilter filter)
        {
            var healthDatas = world.GetObservablePool<HealthData>();
            foreach (var i in filter)
            {
                healthDatas.Modify(i).Health = 0;
            }
        }
    }
}