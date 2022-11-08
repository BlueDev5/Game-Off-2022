using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.DI
{
    public class Binder : IBinder
    {
        #region Variables
        private IList<BindingInfo> _bindings = new List<BindingInfo>();
        #endregion


        #region Getters and Setters

        #endregion


        #region Constructors

        #endregion


        #region Functions
        public void AddBinding(BindingInfo info)
        {
            _bindings.Add(info);
        }

        public IBindingFactory Bind<T>()
        {
            return Bind(typeof(T));
        }

        public IBindingFactory Bind(Type type)
        {
            return CreateBindingFactory(type);
        }

        public bool ContainsBindingsFor<Type>()
        {
            return ContainsBindingsFor(typeof(Type));
        }

        public bool ContainsBindingsFor(Type type)
        {
            return _bindings
                    .Where((BindingInfo info) => info.type == type)
                    .Count() > 0;
        }

        public bool ContainsBindingsFor(object identifier)
        {
            return _bindings
                    .Where((BindingInfo info) => info.identifier == identifier)
                    .Count() > 0;
        }

        public IList<BindingInfo> GetBindings()
        {
            return _bindings;
        }

        public IList<BindingInfo> GetBindingsFor<Type>()
        {
            return GetBindingsFor(typeof(Type));
        }

        public IList<BindingInfo> GetBindingsFor(Type type)
        {
            return _bindings.Where((BindingInfo info) => info.type == type).ToList();
        }

        public IList<BindingInfo> GetBindingsFor(Type type, object identifier)
        {
            return _bindings
                    .Where((BindingInfo info) => info.type == type && info.identifier == identifier)
                    .ToList();
        }

        public IList<BindingInfo> GetBindingsFor(object identifier)
        {
            return _bindings.Where((BindingInfo info) => info.identifier == identifier).ToList();
        }

        public IList<BindingInfo> GetBindingsTo(Type type)
        {
            throw new NotImplementedException();
        }

        public IList<BindingInfo> GetBindingsTo<Type>()
        {
            throw new NotImplementedException();
        }

        public void Unbind<Type>()
        {
            throw new NotImplementedException();
        }

        public void Unbind(Type type)
        {
            throw new NotImplementedException();
        }

        public void Unbind(object identifier)
        {
            throw new NotImplementedException();
        }

        public void UnbindByTag(string tag)
        {
            throw new NotImplementedException();
        }

        public void UnbindInstance(object instance)
        {
            throw new NotImplementedException();
        }

        public IBindingFactory CreateBindingFactory(Type type)
        {
            return new BindingFactory(this, type);
        }
        #endregion
    }
}