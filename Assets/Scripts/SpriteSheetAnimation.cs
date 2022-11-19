using UnityEngine;


namespace Utils
{
    public class SpriteSheetAnimation : MonoBehaviour
    {
        #region Variables
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private int _frameRate;
        [SerializeField] private bool _startAwake;
        [SerializeField] private Sprite[] _sprites;

        private int _currentFrame = 0;
        private float _timeSinceLastFrame = 0;

        private bool _isPlaying;
        private bool _playOneShot;

        public Empty OnAnimationPlayed;
        public Empty OnAnimationStopped;
        #endregion


        #region Getters and Setters
        public bool IsPlaying { get => _isPlaying; }
        #endregion


        #region Unity Calls
        void Awake()
        {
            if (_spriteRenderer == null) _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if (_startAwake) _isPlaying = true;
        }

        void Update()
        {
            if (!_isPlaying) return;

            // Update Frame Number
            UpdateCurrentFrame();

            // Update sprite
            _spriteRenderer.sprite = _sprites[_currentFrame];
        }

        #endregion


        #region Functions
        public void StartPlaying(int from = 0)
        {
            _isPlaying = true;
            _currentFrame = from;
            OnAnimationPlayed?.Invoke();
        }

        public void PlayOneShot(int from = 0)
        {
            StartPlaying(from: from);
            _playOneShot = true;
        }

        public void StopPlaying()
        {
            _isPlaying = false;
            OnAnimationStopped?.Invoke();
        }

        private void UpdateCurrentFrame()
        {
            float timePerFrame = 1f / _frameRate;

            if (_timeSinceLastFrame >= timePerFrame)
            {
                _currentFrame += 1;

                if (_playOneShot && _currentFrame >= _sprites.Length)
                {
                    _playOneShot = false;
                    _currentFrame = _sprites.Length - 1;
                    StopPlaying();

                    return;
                }
                _currentFrame %= _sprites.Length;
                _timeSinceLastFrame = 0;
            }

            _timeSinceLastFrame += Time.deltaTime;
        }
        #endregion
    }
}