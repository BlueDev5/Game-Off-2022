using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;

namespace Utils
{
    public static class DebugUtils
    {
        private static Dictionary<string, Stopwatch> _stopWatches = new Dictionary<string, Stopwatch>();

        public static readonly Dictionary<string, Color> NamesToColor = new Dictionary<string, Color>
        {
            {"red", Color.red},
            {"black", Color.black},
            {"blue", Color.blue},
            {"cyan", Color.cyan},
            {"gray", Color.gray},
            {"green", Color.green},
            {"magenta", Color.magenta},
            {"white", Color.white},
            {"yellow", Color.yellow},
            {"orange", UtilsClass.HexToColor("#ff8400")},
        };

        public static void Log(bool debug, object message)
        {
            if (!debug) return;
            UnityEngine.Debug.Log(message);
        }

        public static void Log(bool debug, object message, Object context)
        {
            if (!debug) return;
            UnityEngine.Debug.Log(message, context);
        }

        public static void LogWarning(bool debug, object message)
        {
            if (!debug) return;
            UnityEngine.Debug.LogWarning(message);
        }

        public static void LogWarning(bool debug, object message, Object context)
        {
            if (!debug) return;
            UnityEngine.Debug.LogWarning(message, context);
        }

        public static void LogError(bool debug, object message)
        {
            if (!debug) return;
            UnityEngine.Debug.LogError(message);
        }

        public static void LogError(bool debug, object message, Object context)
        {
            if (!debug) return;
            UnityEngine.Debug.LogError(message, context);
        }

        public static void BreakEditor(bool debug)
        {
            if (!debug) return;
            UnityEngine.Debug.Break();
        }

        public static void DrawLine(bool debug, Vector3 start, Vector3 end)
        {
            if (!debug) return;
            UnityEngine.Debug.DrawLine(start, end);
        }

        public static void DrawLine(bool debug, Vector3 start, Vector3 end, Color color)
        {
            if (!debug) return;
            UnityEngine.Debug.DrawLine(start, end, color);
        }

        public static void DrawLine(bool debug, Vector3 start, Vector3 end, Color color, float duration)
        {
            if (!debug) return;
            UnityEngine.Debug.DrawLine(start, end, color, duration);
        }

        public static void DrawRay(bool debug, Vector3 start, Vector3 dir)
        {
            if (!debug) return;
            UnityEngine.Debug.DrawRay(start, dir);
        }

        public static void DrawRay(bool debug, Vector3 start, Vector3 dir, Color color)
        {
            if (!debug) return;
            UnityEngine.Debug.DrawRay(start, dir, color);
        }

        public static void DrawRay(bool debug, Vector3 start, Vector3 dir, Color color, float duration)
        {
            if (!debug) return;
            UnityEngine.Debug.DrawRay(start, dir, color, duration);
        }

        public static void StartStopWatch(bool debug, string id)
        {
            Stopwatch sw = new Stopwatch();
            _stopWatches.Add(id, sw);
            sw.Start();
        }

        public static void ResumeStopWatch(bool debug, string id)
        {
            Stopwatch sw = _stopWatches[id];
            if (sw == null) return;
            sw.Start();
        }

        public static void StopStopWatch(bool debug, string id)
        {
            Stopwatch sw = _stopWatches[id];
            if (sw == null) return;
            sw.Stop();
        }

        public static long GetStopWatchTime(bool debug, string id)
        {
            Stopwatch sw = _stopWatches[id];
            if (sw == null) return 0;
            return sw.ElapsedMilliseconds;
        }
    }
}