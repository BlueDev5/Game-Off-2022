using System;
using System.Reflection;

namespace Utils.DI
{
    /// <summary>
    /// Represents a basic reflected type.
    /// </summary>
    public class ReflectedClass
    {
        /// <summary>
        /// Type for which this reflected class is for.
        /// </summary>
        public Type ReflectedType;

        /// <summary>
        /// Constructor for this type.
        /// </summary>
        public ConstructorInfo Constructor;

        /// <summary>Constructor parameters' infos.</summary>
        public ParameterInfo[] ConstructorParameters { get; set; }

        /// <summary>Public InjectProperties of the type that can receive injection.</summary>
        public PropertyInfo[] InjectProperties { get; set; }

        /// <summary>Public Inject Fields of the type that can receive injection.</summary>
        public FieldInfo[] InjectFields { get; set; }

        /// <summary>Public ProviderProperties of the type that can receive injection.</summary>
        public PropertyInfo[] ProviderProperties { get; set; }

        /// <summary>Public ProviderFields of the type that can receive injection.</summary>
        public FieldInfo[] ProviderFields { get; set; }

        public override string ToString()
        {
            return ReflectedType.FullName;
        }
    }
}