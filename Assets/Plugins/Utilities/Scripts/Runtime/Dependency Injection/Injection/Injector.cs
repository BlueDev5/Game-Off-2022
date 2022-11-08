using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Utils.DI
{
    public class Injector : IInjector
    {
        #region Variables
        public IBinder binder { get; private set; }
        public IReflectionCache cache { get; private set; }
        #endregion


        #region Getters and Setters

        #endregion


        #region Constructors
        public Injector(IBinder binder, IReflectionCache cache)
        {
            this.binder = binder;
            this.cache = cache;
        }
        #endregion


        #region Functions
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T), null);
        }

        public T Resolve<T>(object identifier)
        {
            return (T)Resolve(typeof(T), identifier);
        }

        public object Resolve(Type type)
        {
            return Resolve(type, null);
        }

        public object Resolve(object identifier)
        {
            return Resolve(null, identifier);
        }

        public object Resolve(Type type, object identifier)
        {
            var typeIsNull = type == null;
            Type typeToGet = null;
            IList<BindingInfo> bindings = new List<BindingInfo>();

            if (typeIsNull)
            {
                typeToGet = typeof(object);
                bindings = binder.GetBindingsFor(identifier);
            }
            else if (!typeIsNull)
            {
                typeToGet = type;
                bindings = binder.GetBindingsFor(typeToGet);
            }

            if (bindings.Count == 0) return null;
            else if (bindings.Count == 1)
            {
                BindingInfo info = bindings[0];
                var instance = CreateInstance(type, info);
                return instance;
            }
            else if (bindings.Count > 1)
            {
                Debug.LogWarning($"Multiple Bindings For Type: {type?.FullName} and identifier: {identifier?.ToString()} Use ResolveAll instead. Returning null.");
                return null;
            }

            return null;
        }

        private object CreateInstance(Type type, BindingInfo info)
        {
            object instance = null;

            if (info.BindingInstance == BindingInstance.Factory)
            {
                var factory = info.value as IFactory;

                if (factory == null)
                {
                    Debug.LogWarning($"Binding instance type of binding is set to Factory but the passed value is not an IFactory \nBinding:\n    {info}");
                }
                else if (factory != null)
                {
                    instance = factory.Create();
                }
            }
            else if (info.BindingInstance == BindingInstance.Instance)
            {
                instance = Instantiate(info, type);
            }
            else if (info.BindingInstance == BindingInstance.Singleton)
            {
                if (info.value is Type) instance = Instantiate(info, type);
                else if (info.value is not Type) instance = info.value;
            }

            return instance;
        }

        private object Instantiate(BindingInfo info, Type type)
        {
            if (type.IsInterface)
            {
                throw new Exception($"Cannot create type {type.FullName} because it is an interface!");
            }

            var reflectedClass = cache.GetClass(type);
            object instance = null;

            if (reflectedClass.Constructor == null)
            {
                throw new Exception($"{reflectedClass.ReflectedType.FullName} does not have a constructor");
            }

            if (reflectedClass.ConstructorParameters.Length == 0)
            {
                instance = reflectedClass.Constructor.Invoke(new object[] { });
            }
            else if (reflectedClass.ConstructorParameters.Length > 0)
            {
                if (info.value is Type)
                {
                    object[] parameters = GetParametersFromInfo(null, reflectedClass.ConstructorParameters);
                    instance = reflectedClass.Constructor.Invoke(parameters);
                }
                else
                {
                    instance = info.value;
                }
            }

            return instance;
        }

        private object[] GetParametersFromInfo(object instance, ParameterInfo[] constructorParameters)
        {
            object[] parameters = new object[constructorParameters.Length];

            for (int i = 0; i < constructorParameters.Length; i++)
            {
                ParameterInfo parmeter = constructorParameters[i];
                parameters[i] = Resolve(parmeter.ParameterType);
            }

            return parameters;
        }

        public T[] ResolveAll<T>()
        {
            return ResolveAll(typeof(T), null).Cast<T>().ToArray();
        }

        public T[] ResolveAll<T>(object identifier)
        {
            return ResolveAll(typeof(T), identifier).Cast<T>().ToArray();
        }

        public object[] ResolveAll(Type type)
        {
            return ResolveAll(type, null).ToArray();
        }

        public object[] ResolveAll(object identifier)
        {
            return ResolveAll(null, identifier).ToArray();
        }

        public object[] ResolveAll(Type type, object identifier)
        {

            var typeIsNull = type == null;
            Type typeToGet = null;
            IList<BindingInfo> bindings = new List<BindingInfo>();

            if (typeIsNull)
            {
                typeToGet = typeof(object);
                bindings = binder.GetBindingsFor(identifier);
            }
            else if (!typeIsNull)
            {
                typeToGet = type;
                bindings = binder.GetBindingsFor(typeToGet);
            }

            object[] instances = new object[bindings.Count];
            for (int i = 0; i < bindings.Count; i++)
            {
                BindingInfo info = bindings[i];

                var instance = CreateInstance(type, info);
                instances[i] = instance;
            }

            return instances;
        }

        public T Inject<T>(T instance) where T : class
        {
            return (T)Inject(typeof(T), instance);
        }

        public object Inject(Type type, object instance)
        {
            ReflectedClass @class = cache.GetClass(type);

            foreach (var field in @class.InjectFields)
            {
                var injectAttribute = field.GetCustomAttribute<InjectAttribute>();

                var resolution = ResolveAll(field.FieldType, injectAttribute.Identifier);
                object resolvedValue = null;

                if (!(resolution == null || resolution?.Length == 0))
                {
                    resolvedValue = resolution[0];
                }

                field.SetValue(instance, resolvedValue);
            }

            foreach (var property in @class.InjectProperties)
            {
                var injectAttribute = property.GetCustomAttribute<InjectAttribute>();

                var resolution = ResolveAll(property.PropertyType, injectAttribute.Identifier);
                object resolvedValue = null;

                if (!(resolution == null || resolution?.Length == 0))
                {
                    resolvedValue = resolution[0];
                }

                property.SetValue(instance, resolution);
            }

            return instance;
        }

        public T InjectFor<T>(T instance) where T : class
        {
            return (T)InjectFor(typeof(T), instance);
        }

        public object InjectFor(Type type, object instance)
        {
            ReflectedClass @class = cache.GetClass(type);

            foreach (var field in @class.InjectFields)
            {
                if (field.FieldType != type) continue;

                var injectAttribute = field.GetCustomAttribute<InjectAttribute>();

                var resolution = ResolveAll(field.FieldType, injectAttribute.Identifier);
                object resolvedValue = null;

                if (!(resolution == null || resolution?.Length == 0))
                {
                    resolvedValue = resolution[0];
                }

                field.SetValue(instance, resolvedValue);
            }

            foreach (var property in @class.InjectProperties)
            {
                if (property.PropertyType != type) continue;

                var injectAttribute = property.GetCustomAttribute<InjectAttribute>();

                var resolution = ResolveAll(property.PropertyType, injectAttribute.Identifier);
                object resolvedValue = null;

                if (!(resolution == null || resolution?.Length == 0))
                {
                    resolvedValue = resolution[0];
                }

                property.SetValue(instance, resolution);
            }

            return instance;
        }
        #endregion
    }
}