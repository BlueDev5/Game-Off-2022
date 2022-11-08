using System;


namespace Utils.DI
{
    /// <summary>
    /// Factory for creating binding.
    /// </summary>
    public interface IBindingFactory
    {
        /// <summary>
        /// The Binder to register the binding to.
        /// </summary>
        IBinder binder { get; }

        /// <summary>
        /// The type of the binding.
        /// </summary>
        Type bindingType { get; }

        /// <summary>
        /// Create binding to self with transient instance type.
        /// </summary>
        void ToSelf();

        /// <summary>
        /// Create binding to self with singleton instance type.
        /// </summary>
        void ToSelfSingleton();

        /// <summary>
        /// Create binding to self with singleton instance type.
        /// </summary>
        void ToSingleton<T>();

        /// <summary>
        /// Create binding to self with singleton instance type.
        /// </summary>
        void ToSingleton(Type type);

        /// <summary>
        /// Create binding to instance with singleton instance type.
        /// </summary>
        /// <param name="instance"></param>
        void ToSingletonInstance(object instance);

        /// <summary>
        /// Create binding to self with singleton instance type.
        /// </summary>
        void ToSelfInstance(object instance);

        /// <summary>
        /// Creates a binding to <paramref name="factoryInstance"/> with factory Instance Type.
        /// </summary>
        /// <param name="factoryInstance">The instance of the factory to bind to</param>
        void ToFactory(IFactory factoryInstance);

    }
}