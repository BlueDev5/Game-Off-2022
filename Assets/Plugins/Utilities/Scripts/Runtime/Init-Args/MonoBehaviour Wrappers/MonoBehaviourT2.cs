namespace Utils.Init
{
    using Mono = UnityEngine.MonoBehaviour;

    /// <summary>
    /// MonoBehaviour Wrapper implemeneting Initable for 2 generic
    /// </summary>
    public abstract class MonoBehaviour<T1, T2> : Mono, IInitable<T1, T2>
    {
        #region Functions
        public abstract void Init(T1 arg1, T2 arg2);
        #endregion
    }
}