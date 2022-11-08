using UnityEngine;

namespace Utils
{
    public static class StringsExtensions
    {
        public static string Bold(this string text) => $"<b>{text}</b>";
        public static string Italic(this string text) => $"<i>{text}</i>";
        public static string Colorise(this string text, Color color) => $"<color={UtilsClass.ColorToHex(color)}>{text}</color>";
        public static string Colorise(this string text, string colorHex) => $"<color={colorHex}>{text}</color>";
        public static string Size(this string text, int size) => $"<size={size}>{text}</size>";
    }
}