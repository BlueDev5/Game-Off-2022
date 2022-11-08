using System;
using System.Collections.Generic;
using System.Reflection;


namespace Utils.DI
{
    /// <summary>
    /// This class is responsible for caching the values that are required project wide.
    /// </summary>
    public static class Cache
    {
        public static List<Type> Types;
        public static List<Type> IgnoredTypes;
        private static bool _hasInitialised = false;

        public static void RefreshCache(bool forceRefresh)
        {
            if (_hasInitialised && !forceRefresh)
                return;

            Types = new List<Type>();
            IgnoredTypes = new List<Type>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                string name = assembly.GetName().Name;
                if (name.Contains("Unity") || name.Contains("System") || name.Contains("mscorlib") || name.Contains("moq") || name.Contains("Castle"))
                    continue;

                RefreshAssembly(assembly);
            }

            _hasInitialised = true;
        }

        private static void RefreshAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsDefined(typeof(IgnoreInjectionAttribute)))
                    IgnoredTypes.Add(type);
                else
                    Types.Add(type);
            }
        }
    }
}