using UnityEngine;
using GameSystems.Scenes.SceneManagement;


namespace Game.UI
{
    public class MainMenu : MonoBehaviour
    {
        #region Variables
        [SerializeField] private SceneCollection _gameStartLevelCollection;
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
            SceneCollectionHandler.LoadSceneCollection(_gameStartLevelCollection);

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