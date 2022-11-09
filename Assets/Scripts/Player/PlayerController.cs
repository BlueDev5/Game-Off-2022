using System;
using UnityEngine;


namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpHeight = 5f;

        [Tooltip("The number of jumps after the first one.")]
        [SerializeField] private int _extraJumps = 2;

        [SerializeField] private Transform _groundTransform;
        [SerializeField] private LayerMask _whatIsGround;

        private bool _isGrounded;
        private Rigidbody2D _rigidbody;

        private int _jumpsLeft;

        private float _horizontalMovement = 0;
        private bool _isFacingRight = true;
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
            if (_isGrounded)
            {
                _jumpsLeft = _extraJumps;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_jumpsLeft > 0)
                {
                    _rigidbody.velocity = Vector2.up * _jumpHeight;
                    _jumpsLeft--;
                }
                else if (_jumpsLeft == 0 && _isGrounded)
                {
                    _rigidbody.velocity = Vector2.up * _jumpHeight;
                }
            }
        }
        #endregion


        #region Functions
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
            if (collision.TryGetComponent(out MovingLevel movingLevel))
            {
                transform.parent = movingLevel.ParentTransform;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<MovingLevel>())
            {
                transform.parent = null;
            }
        }

        #endregion
    }
}