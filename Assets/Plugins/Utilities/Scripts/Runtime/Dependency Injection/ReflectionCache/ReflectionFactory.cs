using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Debug = UnityEngine.Debug;


namespace Utils.DI
{
    public class ReflectionFactory : IReflectionFactory
    {
        #region Variables

        #endregion


        #region Getters and Setters

        #endregion


        #region Constructors

        #endregion


        #region Functions
        public ReflectedClass Create<T>()
        {
            return Create(typeof(T));
        }

        public ReflectedClass Create(Type type)
        {
            ReflectedClass reflectedClass = new ReflectedClass();

            reflectedClass.ReflectedType = type;

            ConstructorInfo[] construtors = type.GetConstructors();
            ConstructorInfo leastParameterConstructor = null;
            ParameterInfo[] leastParametersArray = leastParameterConstructor?.GetParameters();
            foreach (var constructor in type.GetConstructors())
            {
                ParameterInfo[] constructorParameters = constructor.GetParameters();
                if (leastParameterConstructor == null)
                {
                    leastParameterConstructor = constructor;
                    leastParametersArray = constructorParameters;
                }
                else if (constructorParameters.Length < leastParametersArray.Length)
                {
                    leastParameterConstructor = constructor;
                    leastParametersArray = constructorParameters;
                }
            }

            reflectedClass.Constructor = leastParameterConstructor;
            reflectedClass.ConstructorParameters = leastParametersArray;

            List<FieldInfo> injectFields = new List<FieldInfo>();
            List<FieldInfo> providerFields = new List<FieldInfo>();
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public
                                                | BindingFlags.DeclaredOnly | BindingFlags.Static))
            {
                var injectAttribute = field.GetCustomAttribute<InjectAttribute>();
                var serviceAttribute = field.GetCustomAttribute<ServiceAttribute>();

                if (injectAttribute != null)
                {
                    injectFields.Add(field);
                }
                else if (serviceAttribute != null)
                {
                    providerFields.Add(field);
                }
            }
            reflectedClass.InjectFields = injectFields.ToArray();
            reflectedClass.ProviderFields = providerFields.ToArray();

            var injectProperties = new List<PropertyInfo>();
            var providerProperties = new List<PropertyInfo>();
            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public
                                                | BindingFlags.DeclaredOnly | BindingFlags.Static))
            {
                if (property.IsDefined(typeof(InjectAttribute)))
                {
                    injectProperties.Add(property);
                }
                else if (property.IsDefined(typeof(ServiceAttribute)))
                {
                    providerProperties.Add(property);
                }
            }
            reflectedClass.InjectProperties = injectProperties.ToArray();
            reflectedClass.ProviderProperties = providerProperties.ToArray();

            return reflectedClass;
        }
        #endregion
    }
}