using UnityEngine;


namespace Utils.Init
{
    /// <summary>
    /// Represents a Pair of Components. (This is outputted by UninitializedGameObject.Init2())
    /// </summary>
    public struct Components<TFirstComponent, TSecondComponent>
        where TFirstComponent : Component where TSecondComponent : Component
    {
        #region Variables
        private TFirstComponent _firstComponent;
        private TSecondComponent _secondComponent;
        #endregion


        #region Getters and Setters
        public TFirstComponent FirstComponent => _firstComponent;
        public TSecondComponent SecondComponent => _secondComponent;
        #endregion

        #region Constructors
        public Components(TFirstComponent firstComponent, TSecondComponent secondComponent)
        {
            _firstComponent = firstComponent;
            _secondComponent = secondComponent;
        }
        #endregion


        #region Functions

        #endregion

        #region Conversions
        public static implicit operator TFirstComponent(Components<TFirstComponent, TSecondComponent> @this)
        {
            return @this._firstComponent;
        }

        public static implicit operator TSecondComponent(Components<TFirstComponent, TSecondComponent> @this)
        {
            return @this._secondComponent;
        }
        #endregion
    }
}