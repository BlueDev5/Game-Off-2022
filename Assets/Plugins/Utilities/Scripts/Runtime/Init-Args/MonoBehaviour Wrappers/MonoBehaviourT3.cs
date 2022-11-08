namespace Utils.Init
{
    using Mono = UnityEngine.MonoBehaviour;

    /// <summary>
    /// MonoBehaviour Wrapper implemeneting Initable for 3 generic
    /// </summary>
    public abstract class MonoBehaviour<T1, T2, T3> : Mono, IInitable<T1, T2, T3>
    {
        #region Functions
        public abstract void Init(T1 arg1, T2 arg2, T3 arg3);
        #endregion
    }
}