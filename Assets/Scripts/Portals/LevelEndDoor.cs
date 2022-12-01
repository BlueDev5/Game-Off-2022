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
                if (!controller.HasKey) return;

                //play door sound
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX_gate_open");

                PlayDoorAnimation();

                _hasBeenUsed = true;
            }
        }

        private void OnAnimationStopped()
        {
            _animationSpriteSheets[0].OnAnimationStopped -= OnAnimationStopped;
            Levels.LevelManager.Instance.LoadNextLevel();
        }

        private void PlayDoorAnimation()
        {
            if (_animationSpriteSheets.Length == 0)
            {
                Levels.LevelManager.Instance.LoadNextLevel();
                return;
            }

            _animationSpriteSheets[0].OnAnimationStopped += OnAnimationStopped;

            foreach (var animation in _animationSpriteSheets)
            {
                animation.PlayOneShot();
            }
        }
        #endregion


        #region Functions

        #endregion
    }
}