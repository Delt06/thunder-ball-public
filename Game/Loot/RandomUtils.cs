using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Loot
{
    public static class RandomUtils
    {
        public static TItem GetRandomItemWeighted<TRecord, TItem>(IReadOnlyList<TRecord> records,
            Func<TRecord, TItem> getItem, Func<TRecord, float> getWeight)
        {
            var weightSum = 0f;

            // ReSharper disable once LoopCanBeConvertedToQuery
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < records.Count; index++)
            {
                weightSum += getWeight(records[index]);
            }

            var cumulativeProbability = 0f;
            var randomValue = Random.value;

            // ReSharper disable once LoopCanBeConvertedToQuery
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < records.Count; index++)
            {
                var record = records[index];
                var probability = getWeight(record) / weightSum;
                cumulativeProbability += probability;
                if (cumulativeProbability >= randomValue)
                    return getItem(record);
            }

            return getItem(records[Random.Range(0, records.Count)]);
        }

        public static bool TryProbability(float probability) => Random.value <= probability;

        public static Quaternion RandomRotationY() =>
            Quaternion.Euler(0f, Random.value * 360f, 0f);

        public static int GetRandomInRangeInclusive(this Vector2Int range) => Random.Range(range.x, range.y + 1);

        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static T GetRandomItem<T>(this IReadOnlyList<T> list) => list[Random.Range(0, list.Count)];
    }
}