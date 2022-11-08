using System;


namespace Utils.DI
{
    public class BindingFactory : IBindingFactory
    {
        #region Variables
        public IBinder binder { get; }

        public Type bindingType { get; }
        #endregion


        #region Getters and Setters

        #endregion


        #region Constructors
        public BindingFactory(IBinder binder, Type bindingType)
        {
            this.binder = binder;
            this.bindingType = bindingType;
        }
        #endregion


        #region Functions
        public void ToSelf()
        {
            BindingInfo info = new BindingInfo(bindingType, bindingType, BindingInstance.Instance);
            binder.AddBinding(info);
        }

        public void ToSelfSingleton()
        {
            ToSingleton(bindingType);
        }

        public void ToSingleton<T>()
        {
            ToSingleton(typeof(T));
        }

        public void ToSingleton(Type type)
        {
            BindingInfo info = new BindingInfo(bindingType, type, BindingInstance.Singleton);
            binder.AddBinding(info);
        }

        public void ToSelfInstance(object instance)
        {
            BindingInfo info = new BindingInfo(bindingType, instance, BindingInstance.Instance);
            binder.AddBinding(info);
        }

        public void ToSingletonInstance(object instance)
        {
            BindingInfo info = new BindingInfo(bindingType, instance, BindingInstance.Singleton);
            binder.AddBinding(info);
        }

        public void ToFactory(IFactory factoryInstance)
        {
            BindingInfo info = new BindingInfo(bindingType, factoryInstance, BindingInstance.Factory);
            binder.AddBinding(info);
        }
        #endregion
    }
}