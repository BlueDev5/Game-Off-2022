using System;
using GameSystems.Scenes.SceneManagement;
using UnityEngine;


namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Events
        public event EventHandler OnPlayerJump;
        public class UpdateVelocityArgs : EventArgs
        {
            public float velocityX;
        }

        public event EventHandler<UpdateVelocityArgs> OnPlayerUpdateVelocity;

        public class UpdateGroundedArgs : EventArgs
        {
            public bool onGround;
        }

        public event EventHandler<UpdateGroundedArgs> OnPlayerUpdateGrounded;
        #endregion

        #region Variables
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpHeight = 5f;

        [Tooltip("The number of jumps after the first one.")]
        [SerializeField] private int _extraJumps = 2;

        [SerializeField] private Transform _groundTransform;
        [SerializeField] private LayerMask _whatIsGround;

        [SerializeField] private SceneCollection _deathScreenCollection;

        private bool _isGrounded;
        private Rigidbody2D _rigidbody;

        private int _jumpsLeft;

        private float _horizontalMovement = 0;
        private bool _isFacingRight = true;

        public bool HasKey = false;
        #endregion


        #region Getters and Setters

        #endregion


        #region Unity Calls
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _jumpsLeft = _extraJumps + 1;
        }

        void FixedUpdate()
        {
            OnPlayerUpdateGrounded?.Invoke(this, new UpdateGroundedArgs()
            {
                onGround = _isGrounded
            });

            OnPlayerUpdateVelocity?.Invoke(this, new UpdateVelocityArgs()
            {
                velocityX = _rigidbody.velocity.x
            });

            if (GameplayModeManager.Instance.m_GameplayMode != GameplayMode.Walking)
            {
                return;
            }

            GatherInput();

            _rigidbody.velocity = new Vector2(_horizontalMovement * _speed, _rigidbody.velocity.y);

            if (_isFacingRight && _horizontalMovement < 0)
            {
                Flip();
            }
            else if (!_isFacingRight && _horizontalMovement > 0)
            {
                Flip();
            }
        }

        void Update()
        {
            if (GameplayModeManager.Instance.m_GameplayMode != GameplayMode.Walking)
            {
                return;
            }

            if (_isGrounded)
            {
                _jumpsLeft = _extraJumps;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_jumpsLeft > 0)
                {
                    _rigidbody.velocity = Vector2.up * _jumpHeight;
                    Jump();
                    _jumpsLeft--;
                }
                else if (_jumpsLeft == 0 && _isGrounded)
                {
                    _rigidbody.velocity = Vector2.up * _jumpHeight;
                    Jump();
                }
            }
        }
        #endregion


        #region Functions
        public void Death()
        {
            SceneCollectionHandler.LoadSceneCollection(_deathScreenCollection);
        }

        void Jump()
        {
            OnPlayerJump?.Invoke(this, EventArgs.Empty);
        }

        private void GatherInput()
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal");

            _isGrounded = Physics2D.OverlapBox(_groundTransform.position, _groundTransform.localScale, 0, _whatIsGround);

            _horizontalMovement = horizontalMovement;
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;

            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        #endregion

        #region Triggers interaction

        private void OnTriggerEnter2D(Collider2D collision)
        {
            HandleCameraTarget(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            HandleCameraTarget(collision);
        }

        void HandleCameraTarget(Collider2D collision)
        {
            CameraController.Instance.CheckCameraTarget();
        }

        #endregion
    }
}