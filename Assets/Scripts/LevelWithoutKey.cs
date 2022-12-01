using UnityEngine;


namespace Game.Levels
{
    /// <summary>
    /// Class to be attached on any game object, inside a level which does not require a key to finish it. 
    /// </summary>
    public class LevelWithoutKey : MonoBehaviour
    {
        #region Variables

        #endregion


        #region Getters and Setters

        #endregion


        #region Unity Calls
        void Awake()
        {
            var player = GameObject.FindObjectOfType<Player.PlayerController>();
            player.HasKey = true;
        }
        #endregion


        #region Functions

        #endregion
    }
}