using System;
using UnityEngine;
using Utils;

namespace Game.Enemy
{
    public class EnemyAi : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float _speed;
        [SerializeField] private float _distance;
        [SerializeField] private Transform _wallDetector;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private LayerMask _whatIsWall;
        [SerializeField] private LayerMask _whatIsPlayer;

        [Header("Attack")]
        [SerializeField] private float _shootingRange;
        [SerializeField] private float _timeBetweenAttacks;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Transform _projectileShootPoint;

        [Header("Animations")]
        [SerializeField] private Sprite _idleSprite;
        [SerializeField] private SpriteSheetAnimation _walkAnimation;
        [SerializeField] private SpriteSheetAnimation _shootAnimation;

        private bool _movingRight = false;
        private bool _isShooting = false;

        private float _timeSinceLastAttack = 0f;

        private Player.PlayerController _player;

        #endregion


        #region Getters and Setters

        #endregion


        #region Unity Calls
        void Flip()
        {
            _movingRight = !_movingRight;
            transform.eulerAngles = new Vector3(0, _movingRight ? 180 : 0, 0);
        }

        void Update()
        {
            _isShooting = PlayerInShootingRange();
            if (!_isShooting)
            {
                _timeSinceLastAttack = 0;
            }

            if (_isShooting && _timeSinceLastAttack >= _timeBetweenAttacks)
            {
                Shoot();
            }

            if (ObstacleAhead())
            {
                Flip();
            }

            FacePlayerIfShooting();

            if (!PlayerAhead())
            {
                if (!_walkAnimation.IsPlaying) _walkAnimation.StartPlaying();
                transform.Translate(Vector2.left * _speed * Time.deltaTime);
            }
            else if (_walkAnimation.IsPlaying)
            {
                // Stop Animation.
                _walkAnimation.StopPlaying();

                // Reset the animation to first frame.
                GetComponent<SpriteRenderer>().sprite = _idleSprite;
            }

            _timeSinceLastAttack += Time.deltaTime;
        }

        private void FacePlayerIfShooting()
        {
            if (!_isShooting) return;

            if (_movingRight && transform.position.x > _player.transform.position.x)
            {
                Flip();
            }
            else if (!_movingRight && transform.position.x < _player.transform.position.x)
            {
                Flip();
            }
        }

        void Awake()
        {
            _player = GameObject.FindObjectOfType<Player.PlayerController>();
        }
        #endregion


        #region Functions
        private bool PlayerInShootingRange()
        {
            var xDistance = Mathf.Abs(_player.transform.position.x - transform.position.x);
            var yDistance = Mathf.Abs(_player.transform.position.y - transform.position.y);

            return xDistance < _shootingRange && yDistance < 0.75f;
        }

        private bool ObstacleAhead()
        {
            var obstacleInfo = Physics2D.Raycast(_wallDetector.position, _movingRight ? Vector2.right : Vector2.left, _distance, _whatIsWall);
            return obstacleInfo.collider != null;
        }

        private bool PlayerAhead()
        {

            var playerInfo = Physics2D.Raycast(_wallDetector.position, _movingRight ? Vector2.right : Vector2.left, _distance, _whatIsPlayer);
            return playerInfo.collider != null;
        }

        private bool IsOnGround()
        {
            var groundInfo = Physics2D.Raycast(_wallDetector.position, Vector2.down, 0.1f, _whatIsGround);
            return groundInfo.collider != null;
        }

        private void Shoot()
        {
            _timeSinceLastAttack = 0;

            var projectile = Instantiate(_projectile, _projectileShootPoint.position, Quaternion.identity);
            projectile.Direction = new Vector2(_movingRight ? 1 : -1, 0);

            // Play Shoot Animation.
            _shootAnimation.PlayOneShot();
        }
        #endregion
    }
}