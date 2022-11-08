using System;
using System.Collections.Generic;

namespace Utils.DI
{
    /// <summary>
    /// Interface for all binders.
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// Adds a binding info.
        /// </summary>
        void AddBinding(BindingInfo info);

        /// <summary>
        /// Get All the registered Bindings.
        /// </summary>
        IList<BindingInfo> GetBindings();

        /// <summary>
        /// Get All the bindings registered for <typeparamref name="Type"/>.
        /// </summary>
        /// <typeparam name="Type">The type to search bindings for</typeparam>
        IList<BindingInfo> GetBindingsFor<Type>();

        /// <summary>
        /// Get all the bindings registered for <paramref name="Type"/>
        /// </summary>
        /// <param name="type">The type to search bindings for</param>
        IList<BindingInfo> GetBindingsFor(Type type);

        /// <summary>
        /// Gets all the bindings registered for passed identifier.
        /// </summary>
        IList<BindingInfo> GetBindingsFor(object identifier);

        /// <summary>
        /// Gets all the bindings registered for passed identifier.
        /// </summary>
        IList<BindingInfo> GetBindingsFor(Type type, object identifier);

        /// <summary>
        /// Gets all the bindings registered to <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to search bindings to.</param>
        IList<BindingInfo> GetBindingsTo(Type type);

        /// <summary>
        /// Gets all the bindings registered to <typeparamref name="Type"/>.
        /// </summary>
        /// <typeparam name="Type">The type to search bindings to.</typeparam>
        IList<BindingInfo> GetBindingsTo<Type>();

        /// <summary>
        /// Check if the Binder contains a binding for the given type.
        /// </summary>
        /// <typeparam name="Type">The type to check bindings for.</typeparam>
        /// <returns>True if a binding was found for the passed type else returns false.</returns>
        bool ContainsBindingsFor<Type>();

        /// <summary>
        /// Check if the Binder contains a binding for the given type.
        /// </summary>
        /// <param name="Type">The type to check bindings for.</param>
        /// <returns>True if a binding was found for the passed type else returns false.</returns>
        bool ContainsBindingsFor(Type type);

        /// <summary>
        /// Check if the Binder contains a binding for the given identifier.
        /// </summary>
        /// <param name="identifier"> identifier to check bindings for.</param>
        /// <returns>True if a binding was found for the passed identifier else returns false.</returns>
        bool ContainsBindingsFor(object identifier);

        /// <summary>
        /// Unbinds any bindings registered for <typeparamref name="Type"/>
        /// </summary>
        /// <typeparam name="type">The type to unbind any binding(s) for.</typeparam>
        void Unbind<Type>();

        /// <summary>
        /// Unbinds any bindings registered for <paramref name="Type"/>
        /// </summary>
        /// <param name="type">The type to unbind any binding(s) for.</param>
        void Unbind(Type type);

        /// <summary>
        /// Unbinds any bindings registered for <paramref name="identifier"/>
        /// </summary>
        /// <param name="identifier">identifier to unbind any binding(s) for.</param>
        void Unbind(object identifier);

        /// <summary>
        /// Unbinds any bindings that holds the given instance, either as a value or on conditions.
        /// </summary>
        /// <param name="instance">Instance.</param>
        void UnbindInstance(object instance);

        /// <summary>
        /// Unbinds any bindings that for the given tag
        /// </summary>
        /// <param name="instance">tag.</param>
        void UnbindByTag(string tag);

        /// <summary>
        /// Binds a type to another type or instance.
        /// </summary>
        /// <typeparam name="T">The type to bind to.</typeparam>
        /// <returns>The binding.</returns>
        IBindingFactory Bind<T>();

        /// <summary>
        /// Binds a type to another type or instance.
        /// </summary>
        /// <param name="type">The type to bind to.</param>
        /// <returns>The binding.</returns>
        IBindingFactory Bind(Type type);
    }
}