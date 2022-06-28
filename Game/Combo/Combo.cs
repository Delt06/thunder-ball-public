using System;
using Sirenix.OdinInspector;

namespace Game.Combo
{
    [Serializable]
    public struct Combo
    {
        [HideInEditorMode]
        public int Count;
    }
}