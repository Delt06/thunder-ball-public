using System;

namespace Game.Skills.Burning
{
    [Serializable]
    public struct Burning
    {
        public BurningParams Params;
        public float ElapsedTime;
        public int RemainingTimes;
    }
}