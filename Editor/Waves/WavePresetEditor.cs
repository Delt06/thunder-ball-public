using System.Linq;
using Game.Enemies;
using Game.Waves;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.Waves
{
    [CustomEditor(typeof(WavePreset))]
    public class WavePresetEditor : OdinEditor
    {
        private const float Width = 400;
        private const float HeightUnitsToPixels = 10;
        private const float MinHeightUnits = 10;
        private const float EnemyWidth = 50;
        private const float EnemyHeight = 20;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(50);

            var wavePreset = (WavePreset) target;
            var maxZ = wavePreset.EnemyPlacements.Length > 0 ? wavePreset.EnemyPlacements.Max(ep => ep.Z) : 0f;
            var heightUnits = Mathf.Max(maxZ, MinHeightUnits);
            var height = heightUnits * HeightUnitsToPixels;
            var rect = GUILayoutUtility.GetRect(Width, height, GUILayout.ExpandWidth(false));
            EditorGUI.DrawRect(rect, Color.grey);

            foreach (var enemyPlacement in wavePreset.EnemyPlacements)
            {
                var enemyLocalPosition = GetLocalRectCoordinates(enemyPlacement, heightUnits, height);
                var size = new Vector2(EnemyWidth, EnemyHeight);
                var position = rect.position + enemyLocalPosition;
                position -= size * 0.5f;
                var enemyRect = new Rect(position, size);
                EditorGUI.DrawRect(enemyRect, Color.red);
                EditorGUI.LabelField(enemyRect, GetLabel(enemyPlacement.Enemy), EditorStyles.boldLabel);
            }
        }

        private static string GetLabel([CanBeNull] EnemyPreset enemy) =>
            (enemy ? enemy.name : "null").Replace("Enemy_", string.Empty);

        private static Vector2 GetLocalRectCoordinates(WavePreset.EnemyPlacement enemyPlacement, float heightUnits,
            float height)
        {
            var x = enemyPlacement.X * Width;
            var y = (1 - enemyPlacement.Z / heightUnits) * height;
            return new Vector2(x, y);
        }
    }
}