using UnityEngine;


namespace Utils.Init
{
    /// <summary>
    /// Script for initializing a monoBehaviour Wrapper without accessing the variable directly for 1 arg
    /// Client.
    /// </summary>
    public abstract class Initializer<Client, Arg1> : Initializer where Client : MonoBehaviour<Arg1>, IOneArgument
    {
        #region Variables
        [Header("Client")]
        [SerializeField] private Client _client;

        [Space]

        [Header("Arguments")]
        [SerializeField] private Arg1 _arg;
        #endregion

        #region Unity Calls
        public virtual void Awake()
        {
            _client.Init(_arg);
            Destroy(this);
        }
        #endregion
    }
}