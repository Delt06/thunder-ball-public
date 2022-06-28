using System.Collections.Generic;
using TMPro;

namespace Game.Ui
{
    public static class TmpTextExtensions
    {
        private static readonly Dictionary<int, string> FloatFormats = new();

        public static void SetTextFloat(this TMP_Text text, float value, int? decimalPlaces = null)
        {
            var format = decimalPlaces == null ? "{0}" : GetFloatFormat(decimalPlaces.Value);
            text.SetText(format, value);
        }

        private static string GetFloatFormat(int decimalPlaces)
        {
            if (FloatFormats.TryGetValue(decimalPlaces, out var format))
                return format;
            format = $"{{0:{decimalPlaces}}}";
            FloatFormats.Add(decimalPlaces, format);
            return format;
        }
    }
}