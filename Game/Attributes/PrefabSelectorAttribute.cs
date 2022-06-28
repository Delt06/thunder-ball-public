using System;
using Sirenix.OdinInspector;

namespace Game.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    [IncludeMyAttributes]
    [AssetSelector(Paths = "Assets/Core")]
    public class PrefabSelectorAttribute : Attribute { }
}