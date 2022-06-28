using UnityEngine;

namespace Game.VFX.Parenting
{
    public struct CustomParenting
    {
        public Transform Parent;
        public Vector3 LocalPosition;
        public Quaternion LocalRotation;
        public bool IsValid;
    }
}