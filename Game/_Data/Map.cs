using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game._Data
{
    [Serializable] [InlineProperty]
    public struct Map<TKey, TValue>
    {
        [HideLabel]
        [SerializeField] [TableList] private Row[] _rows;

        public TValue this[TKey key]
        {
            get
            {
                foreach (var row in _rows)
                {
                    if (key.Equals(row.Key))
                        return row.Value;
                }

                throw new ArgumentException($"Element with key {key} not found");
            }
        }

        [Serializable]
        private struct Row
        {
            public TKey Key;
            public TValue Value;
        }
    }
}