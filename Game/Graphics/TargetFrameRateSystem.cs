using Game._Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Graphics
{
    public class TargetFrameRateSystem : IEcsInitSystem
    {
        private readonly StaticData _staticData;

        public TargetFrameRateSystem(StaticData staticData) => _staticData = staticData;

        public void Init(EcsSystems systems)
        {
            Application.targetFrameRate = _staticData.TargetFrameRate;
        }
    }
}