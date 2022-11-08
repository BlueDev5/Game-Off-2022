using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils.DI
{
    public static class DIManager
    {
        public static IBinder Binder;
        public static IInjector Injector;
        private static IReflectionCache _reflectionCache;
        private static IReflectionFactory _reflectionFactory;

        public static void Initialize()
        {
            Binder = new Binder();
            _reflectionFactory = new ReflectionFactory();
            _reflectionCache = new ReflectionCache(_reflectionFactory);
            Injector = new Injector(Binder, _reflectionCache);

            Cache.RefreshCache(false);

            BindAll();
            InjectAll();
        }

        /// <summary>
        /// Bind all the service attributes to their respective types.
        /// </summary>
        public static void BindAll()
        {
            foreach (var type in Cache.Types)
            {
                var @class = _reflectionCache.GetClass(type);

                if (!(typeof(Object).IsAssignableFrom(type)) || type.IsAbstract || type.IsGenericType) continue;

                var instance = GameObject.FindObjectOfType(type);

                BindFields(@class.ProviderFields, instance);
                BindProperties(@class.ProviderProperties, instance);
            }
        }

        private static void BindFields(FieldInfo[] fields, Object instance)
        {
            foreach (var field in fields)
            {
                ServiceAttribute attribute = field.GetCustomAttribute<ServiceAttribute>();
                IBindingFactory bindingFactory = Binder.Bind(field.FieldType);

                if (attribute.BindingInstance == BindingInstance.Instance)
                {
                    object value = field.GetValue(instance);
                    bindingFactory.ToSelfInstance(value);
                }
                else if (attribute.BindingInstance == BindingInstance.Singleton)
                {
                    object value = field.GetValue(instance);
                    bindingFactory.ToSingletonInstance(value);
                }
            }
        }

        private static void BindProperties(PropertyInfo[] properties, Object instance)
        {
            foreach (var property in properties)
            {
                ServiceAttribute attribute = property.GetCustomAttribute<ServiceAttribute>();
                IBindingFactory bindingFactory = Binder.Bind(property.PropertyType);

                if (attribute.BindingInstance == BindingInstance.Instance)
                {
                    object value = property.GetValue(instance);
                    bindingFactory.ToSelfInstance(value);
                }
                else if (attribute.BindingInstance == BindingInstance.Singleton)
                {
                    object value = property.GetValue(instance);
                    bindingFactory.ToSingletonInstance(value);
                }
            }
        }

        /// <summary>
        /// Inject all the inject Attribute fields/properties.
        /// </summary>
        public static void InjectAll()
        {
            foreach (var type in Cache.Types)
            {
                if (!(typeof(Object).IsAssignableFrom(type)) || type.IsAbstract || type.IsGenericType) continue;

                var instances = GameObject.FindObjectsOfType(type);

                Action<Object> inject = instance => Injector.Inject(type, instance);
                Array.ForEach(instances, inject);
            }
        }

        /// <summary>
        /// Inject all the injectAttributes of the provided type.
        /// </summary>
        /// <typeparam name="T">The type to traverse and inject fields/properties to.</typeparam>
        public static void InjectAll<T>() where T : Object
        {
            InjectAll(typeof(T));
        }

        /// <summary>
        /// Inject all the injectAttributes of the provided type.
        /// </summary>
        /// <param name="T">The type to traverse and inject fields/properties to.</param>
        public static void InjectAll(Type injectType)
        {
            if (!(typeof(Object).IsAssignableFrom(injectType)) || injectType.IsAbstract || injectType.IsGenericType) return;

            var instances = GameObject.FindObjectsOfType(injectType);

            Action<Object> inject = instance => Injector.Inject(injectType, instance);
            Array.ForEach(instances, inject);
        }

        /// <summary>
        /// Inject all the injectAttributes that require the provided type.
        /// </summary>
        /// <typeparam name="T">The type of fields/properties to inject to.</typeparam>
        public static void InjectAllFor<T>()
        {
            InjectAll(typeof(T));
        }

        /// <summary>
        /// Inject all the injectAttributes that require the provided type.
        /// </summary>
        /// <param name="T">The type of fields/properties to inject to.</param>
        public static void InjectAllFor(Type bindType)
        {
            foreach (var type in Cache.Types)
            {
                if (!(typeof(Object).IsAssignableFrom(type)) || type.IsAbstract || type.IsGenericType) continue;

                var instances = GameObject.FindObjectsOfType(type);

                Action<Object> inject = instance => Injector.Inject(type, instance);
                Array.ForEach(instances, inject);
            }
        }
    }
}