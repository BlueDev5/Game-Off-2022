using System;


namespace Utils.DI
{
    public class ServiceAttribute : DependencyAttribute
    {
        private readonly object _identifier;
        private readonly BindingInstance _bindingInstance;

        public ServiceAttribute(BindingInstance instance, object identifier = null)
        {
            _bindingInstance = instance;
            _identifier = identifier;
        }

        public object Identifier => _identifier;
        public BindingInstance BindingInstance => _bindingInstance;
    }
}