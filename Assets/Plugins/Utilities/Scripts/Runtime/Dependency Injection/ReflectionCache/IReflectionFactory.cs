using System;

namespace Utils.DI
{
    /// <summary>
    /// Interface for Reflection Factories.
    /// </summary>
    public interface IReflectionFactory
    {
        /// <summary>
        /// Create A reflected class for Type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>The reflected class</returns>
        ReflectedClass Create<T>();

        /// <summary>
        /// Create A reflected class for Type <paramref name="type"/>.
        /// </summary>
        /// <returns>The reflected class</returns>
        ReflectedClass Create(Type type);
    }
}