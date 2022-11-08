using System;
using System.Collections.Generic;

namespace Utils.DI
{
    /// <summary>
    /// Interface for implementing reflection cache.
    /// </summary>
    public interface IReflectionCache
    {
        /// <summary>
        /// Gets the reflected Type with the specified type.
        /// </summary>
        ReflectedClass this[Type type] { get; }

        /// <summary>
        /// The reflection factory to use for creating reflected classes.
        /// </summary>
        IReflectionFactory ReflectionFactory { get; set; }

        /// <summary>
        /// Add a type to the cache
        /// </summary>
        void AddType(Type type);

        /// <summary>
        /// remove a type from the cache
        /// </summary>
        void RemoveType(Type type);

        /// <summary>
        /// Gets an <see cref="Utils.DI.IReflectedClass"/> for a certain type.
        /// </summary>
        /// <remarks>If the type being getted doesn't exist, it'll be created.</remarks>
        /// <param name="type">Type to look for.</param>
        /// <returns>The reflected class.</returns>
        ReflectedClass GetClass(Type type);

        /// <summary>
        /// Checks whether a cache exists for a certain type.
        /// </summary>
        /// <param name="type">Type to be removed.</param>
        /// <returns>Boolean.</returns>
        bool Contains(Type type);
    }
}