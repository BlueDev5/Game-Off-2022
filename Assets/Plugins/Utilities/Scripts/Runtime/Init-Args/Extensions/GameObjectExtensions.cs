using UnityEngine;

namespace Utils.Init
{
    /// <summary>
    /// Extensions for GameObjects.
    /// </summary>
    public static class GameObjectExtensions
    {
        public static void AddComponent<Type, Arg1>(this GameObject gameObject, Arg1 arg) where Type : MonoBehaviour, IInitable<Arg1>
        {
            var type = (Type)gameObject.AddComponent(typeof(Type));
            type.Init(arg);
        }

        public static void AddComponent<Type, Arg1, Arg2>(this GameObject gameObject, Arg1 arg1, Arg2 arg2)
                                        where Type : MonoBehaviour, IInitable<Arg1, Arg2>
        {
            var type = (Type)gameObject.AddComponent(typeof(Type));
            type.Init(arg1, arg2);
        }

        public static void AddComponent<Type, Arg1, Arg2, Arg3>(this GameObject gameObject, Arg1 arg1, Arg2 arg2, Arg3 arg3)
                                        where Type : MonoBehaviour, IInitable<Arg1, Arg2, Arg3>
        {
            var type = (Type)gameObject.AddComponent(typeof(Type));
            type.Init(arg1, arg2, arg3);
        }

        public static void AddComponent<Type, Arg1, Arg2, Arg3, Arg4>(this GameObject gameObject, Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4)
                                        where Type : MonoBehaviour, IInitable<Arg1, Arg2, Arg3, Arg4>
        {
            var type = (Type)gameObject.AddComponent(typeof(Type));
            type.Init(arg1, arg2, arg3, arg4);
        }

        public static void AddComponent<Type, Arg1, Arg2, Arg3, Arg4, Arg5>(this GameObject gameObject, Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4, Arg5 arg5)
                                        where Type : MonoBehaviour, IInitable<Arg1, Arg2, Arg3, Arg4, Arg5>
        {
            var type = (Type)gameObject.AddComponent(typeof(Type));
            type.Init(arg1, arg2, arg3, arg4, arg5);
        }
    }
}