using System;
using System.Collections.Generic;
using System.Reflection;


namespace Utils.Injection
{
    /// <summary>
    /// This class is responsible for caching the values that are required project wide.
    /// </summary>
    public class Cache<AttributeType> : ICache where AttributeType : Attribute
    {
        public List<Type> Types => _types;
        public List<Type> IgnoredTypes => _ignoredTypes;

        private List<Type> _types;
        private List<Type> _ignoredTypes;
        private bool _hasInitialised = false;

        public void RefreshCache(bool forceRefresh)
        {
            if (_hasInitialised && !forceRefresh)
                return;

            _types = new List<Type>();
            _ignoredTypes = new List<Type>();

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

        private void RefreshAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsDefined(typeof(AttributeType)))
                    IgnoredTypes.Add(type);
                else
                    Types.Add(type);
            }
        }

        public Cache(bool forceRefresh = false)
        {
            RefreshCache(forceRefresh);
        }
    }
}
