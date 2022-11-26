using UnityEngine;


namespace Game.Enemy
{
    public class Projectile : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float _speed;
        public Vector2 Direction;
        [SerializeField] private LayerMask _whatIsObstacle;
        #endregion


        #region Getters and Setters

        #endregion


        #region Unity Calls
        void Update()
        {
            transform.Translate(Direction * _speed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Player.PlayerController>(out Player.PlayerController controller))
            {
                controller.Death();
            }

            if ((_whatIsObstacle.value & (1 << other.gameObject.layer)) > 0)
            {
                Destroy(gameObject);
            }
        }

        void Awake()
        {
            Destroy(gameObject, 2f);
        }
        #endregion


        #region Functions

        #endregion
    }
}