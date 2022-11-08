using UnityEngine;


namespace Utils.Init
{
    /// <summary>
    /// Script for initializing a monoBehaviour Wrapper without accessing the variable directly for 5 arg
    /// Client.
    /// </summary>
    public abstract class Initializer<Client, Arg1, Arg2, Arg3, Arg4, Arg5> : Initializer
                        where Client : MonoBehaviour<Arg1, Arg2, Arg3, Arg4, Arg5>, IFiveArguments
    {
        #region Variables
        [Header("Client")]
        [SerializeField] private Client _client;

        [Space]

        [Header("Arguments")]
        [SerializeField] private Arg1 _arg1;
        [SerializeField] private Arg2 _arg2;
        [SerializeField] private Arg3 _arg3;
        [SerializeField] private Arg4 _arg4;
        [SerializeField] private Arg5 _arg5;
        #endregion

        #region Unity Calls
        public virtual void Awake()
        {
            _client.Init(_arg1, _arg2, _arg3, _arg4, _arg5);
            Destroy(this);
        }
        #endregion
    }
}