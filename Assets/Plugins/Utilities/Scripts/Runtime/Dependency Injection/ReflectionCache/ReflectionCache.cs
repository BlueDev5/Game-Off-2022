using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.DI
{
    public class ReflectionCache : IReflectionCache
    {
        #region Variables
        private List<ReflectedClass> _reflectedClasses;

        public IReflectionFactory ReflectionFactory { get; set; }
        #endregion


        #region Getters and Setters
        public ReflectedClass this[Type type] =>
            _reflectedClasses.First((ReflectedClass @class) => @class.ReflectedType == type);
        #endregion


        #region Constructors
        public ReflectionCache(IReflectionFactory reflectionFactory)
        {
            ReflectionFactory = reflectionFactory;
            _reflectedClasses = new List<ReflectedClass>();
        }
        #endregion


        #region Functions
        public void AddType(Type type)
        {
            var reflectedClass = ReflectionFactory.Create(type);
            _reflectedClasses.Add(reflectedClass);
        }

        public bool Contains(Type type)
        {
            return _reflectedClasses
                        .Where((ReflectedClass @class) => @class.ReflectedType == type)
                        .Count() > 0;
        }

        public ReflectedClass GetClass(Type type)
        {
            if (!Contains(type))
            {
                AddType(type);
            }
            return _reflectedClasses.Where((ReflectedClass @class) => @class.ReflectedType == type).First();
        }

        public void RemoveType(Type type)
        {
            _reflectedClasses.RemoveAll((ReflectedClass @class) => @class.ReflectedType == type);
        }
        #endregion
    }
}