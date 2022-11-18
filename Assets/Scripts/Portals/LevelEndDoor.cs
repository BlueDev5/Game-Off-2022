using Game.Player;
using UnityEngine;


namespace Game.Portals
{
    public class LevelEndDoor : MonoBehaviour
    {
        #region Variables

        #endregion


        #region Getters and Setters

        #endregion


        #region Unity Calls
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController controller))
            {
                if (!controller.hasKey) return;

                Debug.Log("Open Door");
                // * Add logic for playing open door animation.
            }
        }
        #endregion


        #region Functions

        #endregion
    }
}