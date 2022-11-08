namespace Utils.Init
{
    using Mono = UnityEngine.MonoBehaviour;

    /// <summary>
    /// MonoBehaviour Wrapper implemeneting Initable for 4 generic
    /// </summary>
    public abstract class MonoBehaviour<T1, T2, T3, T4> : Mono, IInitable<T1, T2, T3, T4>
    {
        #region Function
        public abstract void Init(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        #endregion
    }
}