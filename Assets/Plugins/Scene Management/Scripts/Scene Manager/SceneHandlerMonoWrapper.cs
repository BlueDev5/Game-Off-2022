using UnityEngine;


namespace GameSystems.Scenes.SceneManagement
{
    public class SceneHandlerMonoWrapper : MonoBehaviour
    {
        #region Variables

        #endregion


        #region Getters and Setters

        #endregion


        #region Unity Calls

        #endregion


        #region Functions
        public void LoadSceneAt(int index)
        {
            SceneCollectionHandler.LoadSceneCollection(index);
        }

        public void LoadSceneCollection(SceneCollection collectionRef)
        {
            SceneCollectionHandler.LoadSceneCollection(collectionRef);
        }

        public void UnloadCurrentLoadedCollection(bool forceReload)
        {
            SceneCollectionHandler.UnloadCurrentLoadedCollection(forceReload);
        }
        #endregion
    }
}