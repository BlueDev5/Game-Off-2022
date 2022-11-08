using UnityEngine;


namespace Utils.Init
{
    /// <summary>
    /// Script for initializing a monoBehaviour Wrapper without accessing the variable directly for 2 arg
    /// Client.
    /// </summary>
    public abstract class Initializer<Client, Arg1, Arg2> : Initializer
                        where Client : MonoBehaviour<Arg1, Arg2>, ITwoArguments
    {
        #region Variables
        [Header("Client")]
        [SerializeField] private Client _client;

        [Space]

        [Header("Arguments")]
        [SerializeField] private Arg1 _arg1;
        [SerializeField] private Arg2 _arg2;
        #endregion

        #region Unity Calls
        public virtual void Awake()
        {
            _client.Init(_arg1, _arg2);
            Destroy(this);
        }
        #endregion
    }
}