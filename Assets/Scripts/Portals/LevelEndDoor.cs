using System;
using Game.Player;
using UnityEngine;
using Utils;

namespace Game.Portals
{
    public class LevelEndDoor : MonoBehaviour
    {
        #region Variables
        private SpriteSheetAnimation[] _animationSpriteSheets;
        private bool _hasBeenUsed = false;
        #endregion


        #region Getters and Setters

        #endregion


        #region Unity Calls
        void Awake()
        {
            _animationSpriteSheets = GetComponentsInChildren<SpriteSheetAnimation>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (_hasBeenUsed) return;

            if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
            {
                if (!controller.hasKey) return;

                foreach (var animation in _animationSpriteSheets)
                {
                    animation.PlayOneShot();
                }
                _hasBeenUsed = true;
            }
        }
        #endregion


        #region Functions

        #endregion
    }
}