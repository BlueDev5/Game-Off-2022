using UnityEngine;

namespace GameSystems.Scenes.SceneManagement
{
    public class SceneCollectionHandlerInitializer : MonoBehaviour
    {
        #region Variables
        [SerializeField] private SceneManagerConfig _config;
        #endregion


        #region Getters and Setters

        #endregion


        #region  Unity Calls
        private void Start()
        {
            SceneCollectionHandler.Initialize(_config.StartingSceneIndex, _config.SceneCollections.ToArray(), this);
        }
        #endregion


        #region Functions

        #endregion


        #region Constructors

        #endregion
    }
}