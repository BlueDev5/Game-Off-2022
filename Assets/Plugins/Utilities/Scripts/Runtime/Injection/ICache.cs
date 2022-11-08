using System;
using System.Collections.Generic;

namespace Utils.Injection
{
    public interface ICache
    {
        List<Type> Types { get; }
        List<Type> IgnoredTypes { get; }

        void RefreshCache(bool forceRefresh = false);
    }
}