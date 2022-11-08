using System;


namespace Utils.DI
{
    public class InjectAttribute : DependencyAttribute
    {
        private readonly object _identifier;

        public InjectAttribute()
        {
            _identifier = null;
        }

        public InjectAttribute(object identifier)
        {
            _identifier = identifier;
        }

        public object Identifier => _identifier;
    }
}