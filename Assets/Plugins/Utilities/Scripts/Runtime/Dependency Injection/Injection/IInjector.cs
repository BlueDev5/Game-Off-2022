using System;

namespace Utils.DI
{
    /// <summary>
    /// Interface for Injectors.
    /// </summary>
    public interface IInjector
    {
        /// <summary>
        /// Resolve the dependency of the given type.
        /// </summary>
        /// <remarks> If type has multiple instances use Resolve all. </remarks>
        /// <returns>The instance or null.</returns>
        T Resolve<T>();

        /// <summary>
        /// Resolve the dependency of the given type.
        /// </summary>
        /// <remarks> If type has multiple instances use Resolve all. </remarks>
        /// <returns>The instance or null.</returns>
        T Resolve<T>(object identifier);

        /// <summary>
        /// Resolve the dependency of the given type.
        /// </summary>
        /// <remarks> If type has multiple instances use Resolve all. </remarks>
        /// <returns>The instance or null.</returns>
        object Resolve(Type type);

        /// <summary>
        /// Resolve the dependency of the given type.
        /// </summary>
        /// <remarks> If type has multiple instances use Resolve all. </remarks>
        /// <returns>The instance or null.</returns>
        object Resolve(object identifier);

        /// <summary>
        /// Resolve the dependency of the given type.
        /// </summary>
        /// <remarks> If type has multiple instances use Resolve all. </remarks>
        /// <returns>The instance or null.</returns>
        object Resolve(Type type, object identifier);

        /// <summary>
        /// Resolves a list of instances of the given type.
        /// </summary>
        /// <returns>The list of instance or null.</returns>
        T[] ResolveAll<T>();

        /// <summary>
        /// Resolves a list of instances of the given identifier.
        /// </summary>
        /// <returns>The list of instance or null.</returns>
        T[] ResolveAll<T>(object identifier);

        /// <summary>
        /// Resolves a list of instances of the given type.
        /// </summary>
        /// <returns>The list of instances or null.</returns>
        object[] ResolveAll(Type type);

        /// <summary>
        /// Resolves a list of instances of the given type.
        /// </summary>
        /// <returns>The list of instance or null.</returns>
        object[] ResolveAll(object identifier);

        /// <summary>
        /// Resolves a list of instances of the given type.
        /// </summary>
        /// <returns>The list of instance or null.</returns>
        object[] ResolveAll(Type type, object identifier);

        /// <summary>
        /// Injects the dependencies of the given type.
        /// </summary>
        /// <param name="instance">the instance to receive injection.</param>
        /// <typeparam name="T">the type of the instance to inject dependencies.</typeparam>
        /// <returns>The instance with dependencies injected</returns>
        T Inject<T>(T instance) where T : class;

        /// <summary>
        /// Injects the dependencies of the given type.
        /// </summary>
        /// <param name="instance">the instance to receive injection.</param>
        /// <returns>The instance with dependencies injected</returns>
        object Inject(Type type, object instance);

        /// <summary>
        /// Injects the dependencies that need the given type.
        /// </summary>
        /// <param name="instance">the instance to receive injection.</param>
        /// <typeparam name="T">the type of the instance to inject dependencies.</typeparam>
        /// <returns>The instance with dependencies injected</returns>
        T InjectFor<T>(T instance) where T : class;

        /// <summary>
        /// Injects the dependencies of the given type.
        /// </summary>
        /// <param name="instance">the instance to receive injection.</param>
        /// <returns>The instance with dependencies injected</returns>
        object InjectFor(Type type, object instance);
    }
}