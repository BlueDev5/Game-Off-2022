using UnityEngine;


namespace Utils.Init
{
    /// <summary>
    /// UnityEngine.GameObject Wrapper with 2 Components
    /// </summary>
    public class GameObject<TFirstComponent, TSecondComponent>
            where TFirstComponent : Component where TSecondComponent : Component
    {
        #region Variables
        private GameObject _gameObject;
        #endregion


        #region Getters and Setters
        public GameObject GetGameObjectData() => _gameObject;
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
        public static implicit operator TFirstComponent(GameObject<TFirstComponent, TSecondComponent> @this)
        {
            return @this._gameObject.GetComponent<TFirstComponent>();
        }

        public static implicit operator TSecondComponent(GameObject<TFirstComponent, TSecondComponent> @this)
        {
            return @this._gameObject.GetComponent<TSecondComponent>();
        }

        public static implicit operator (TFirstComponent, TSecondComponent)(GameObject<TFirstComponent, TSecondComponent> @this)
        {
            return (@this._gameObject.GetComponent<TFirstComponent>(), @this._gameObject.GetComponent<TSecondComponent>());
        }
        #endregion
    }
}