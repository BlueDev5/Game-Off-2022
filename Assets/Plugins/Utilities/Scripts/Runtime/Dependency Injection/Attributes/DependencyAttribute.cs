using System;


namespace Utils.DI
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DependencyAttribute : Attribute
    {
        public DependencyAttribute()
        {
        }
    }
}