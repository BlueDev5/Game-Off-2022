using UnityEngine;


namespace Utils.Init
{
    /// <summary>
    /// GameObject Wrapper with 1 Component
    /// </summary>
    public class GameObject<TComponent> : IOneArgument where TComponent : Component
    {
        #region Variables
        private GameObject _gameObject;
        #endregion


        #region Getters and Setters
        public GameObject getGameObjectData() => _gameObject;
        #endregion


        #region Constructors
        public GameObject()
        {
            _gameObject = new GameObject();
        }

        public GameObject(string name)
        {
            _gameObject = new GameObject(name);
        }
        #endregion


        #region Functions
        #endregion

        #region Conversions
        public static implicit operator TComponent(GameObject<TComponent> @this)
        {
            var component = @this._gameObject.GetComponent<TComponent>();
            return component;
        }
        #endregion
    }
}