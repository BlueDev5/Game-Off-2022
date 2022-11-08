using System;
using UnityEngine;

namespace Utils
{
    public static class UtilsClass
    {
        public static string ColorToHex(Color color)
        {
            string r = Mathf.RoundToInt(color.r * 255).ToString("X2");
            string g = Mathf.RoundToInt(color.g * 255).ToString("X2");
            string b = Mathf.RoundToInt(color.b * 255).ToString("X2");
            string a = Mathf.RoundToInt(color.a * 255).ToString("X2");
            return $"#{r}{g}{b}{a}";
        }

        public static Color HexToColor(string hex)
        {
            if (hex.StartsWith("#")) hex = hex.Remove(0, 1);
            float r = HexToFloatNormalized(hex.Substring(0, 2));
            float g = HexToFloatNormalized(hex.Substring(2, 2));
            float b = HexToFloatNormalized(hex.Substring(4, 2));
            if (hex.Length > 6)
            {
                float a = HexToFloatNormalized(hex.Substring(6, 2));
                return new Color(r, g, b, a);
            }
            return new Color(r, g, b);
        }

        private static float HexToFloatNormalized(string hex)
        {
            int intvalue = Convert.ToInt32(hex, 16);
            return intvalue / 255f;
        }
    }
}