using UnityEngine;

namespace Game.Player.Controls
{
    public struct PlayerInputData
    {
        public float PressPlayerPosition;
        public Vector2 Direction;
        public Vector2? ReleaseDirection;
        public Vector2 DirectionUnclamped;
    }
}