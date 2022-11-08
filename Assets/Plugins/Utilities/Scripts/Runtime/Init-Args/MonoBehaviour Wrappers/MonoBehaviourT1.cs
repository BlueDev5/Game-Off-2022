namespace Utils.Init
{
    using Mono = UnityEngine.MonoBehaviour;

    /// <summary>
    /// MonoBehaviour Wrapper implemeneting Initable for 1 generic
    /// </summary>
    public abstract class MonoBehaviour<T1> : Mono, IInitable<T1>
    {
        #region Functions
        public abstract void Init(T1 value);
        #endregion
    }
}