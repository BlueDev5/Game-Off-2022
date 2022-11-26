using UnityEngine;
using GameSystems.Scenes.SceneManagement;


namespace Game.UI
{
    public class MainMenu : MonoBehaviour
    {
        #region Variables
        #endregion


        #region Getters and Setters

        #endregion


        #region Unity Calls

        #endregion


        #region Functions
        // * Implement Start Level
        public void StartLevel()
        {
            // Load Level.
            Levels.LevelManager.Instance.LoadLevel(0);

            // Set State to the Gameplay Mode Manager.
            GameplayModeManager.Instance.SetState(GameplayMode.Walking);
        }

        public void Quit()
        {
#if UNITY_EDITOR
            Debug.Log("Quit App");
#else
            Application.Quit();
#endif
        }
        #endregion
    }
}