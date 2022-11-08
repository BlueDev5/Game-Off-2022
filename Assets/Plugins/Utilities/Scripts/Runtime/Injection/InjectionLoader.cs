using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Injection
{
    public class InjectionLoader<T>
    {
        #region Variables
        private IEnumerable<T> _injectedInstances;
        private IEnumerable<Type> _injectableTypes;
        private ICache _cache;
        #endregion


        #region Getters And Setters

        #endregion


        #region Class Functions
        public IEnumerable<Type> GetInjectableTypes(bool forceReload = false)
        {
            if (!forceReload)
            {
                return _injectableTypes;
            }

            _injectableTypes = new List<Type>();

            for (int i = 0; i < _cache.Types.Count; i++)
            {
                Type type = _cache.Types[i];

                if (type.IsGenericType || type.IsInterface || type.IsEnum)
                {
                    continue;
                }


                if (typeof(T).IsAssignableFrom(type))
                {
                    _injectableTypes = _injectableTypes.Append(type);
                }
            }

            return _injectableTypes;
        }

        public IEnumerable<T> GetInjectedInstances(bool forceReload = false)
        {
            if (!forceReload)
            {
                return _injectedInstances;
            }

            GetInjectableTypes(forceReload);
            _injectedInstances = new List<T>();

            for (int i = 0; i < _injectableTypes.Count(); i++)
            {
                Type typeToInject = _injectableTypes.ElementAt(i);
                _injectedInstances = _injectedInstances.Append<T>((T)Activator.CreateInstance(typeToInject));
            }

            return _injectedInstances;
        }
        #endregion


        #region Constructors
        public InjectionLoader(ICache cache)
        {
            _injectableTypes = new List<Type>();
            _injectedInstances = new List<T>();
            _cache = cache;

            GetInjectableTypes(true);
            GetInjectedInstances(true);
        }
        #endregion
    }
}