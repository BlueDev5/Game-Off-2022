using UnityEngine;


namespace Utils.Init
{
    /// <summary>
    /// GameObject Wrapper representing a gameObject with 1 out of 2 components initalized.
    /// </summary>
    public class UninitializedGameObject<TFirstComponent, TSecondComponent>
        where TFirstComponent : Component where TSecondComponent : Component
    {
        #region Variables
        private GameObject _gameObject;
        #endregion


        #region Getters and Setters
        public GameObject GetGameObjectData() => _gameObject;
        #endregion


        #region Constructors
        public UninitializedGameObject(GameObject gameObject)
        {
            _gameObject = gameObject;
        }
        #endregion


        #region Functions

        #endregion

        #region Conversions
        public static implicit operator TFirstComponent(UninitializedGameObject<TFirstComponent, TSecondComponent> @this)
        {
            return @this._gameObject.GetComponent<TFirstComponent>();
        }

        public static implicit operator TSecondComponent(UninitializedGameObject<TFirstComponent, TSecondComponent> @this)
        {
            return @this._gameObject.GetComponent<TSecondComponent>();
        }

        public static implicit operator (TFirstComponent, TSecondComponent)(UninitializedGameObject<TFirstComponent, TSecondComponent> @this)
        {
            return (@this._gameObject.GetComponent<TFirstComponent>(), @this._gameObject.GetComponent<TSecondComponent>());
        }
        #endregion
    }
}