using System;


namespace Utils.DI
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class IgnoreInjectionAttribute : Attribute
    {
    }
}