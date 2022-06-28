using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemies.Models
{
    [Serializable]
    public struct ModelSlot
    {
        [Required]
        public Transform Slot;
        public GameObject Model;
    }
}