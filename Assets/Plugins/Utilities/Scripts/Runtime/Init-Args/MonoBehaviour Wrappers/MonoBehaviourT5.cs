namespace Utils.Init
{
    using Mono = UnityEngine.MonoBehaviour;

    public abstract class MonoBehaviour<T1, T2, T3, T4, T5> : Mono, IInitable<T1, T2, T3, T4, T5>
    {
        #region Functions
        public abstract void Init(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
        #endregion
    }
}