using System.Collections.Generic;
using GameSystems.Scenes.SceneManagement;
using UnityEngine;


namespace Game.Levels
{
    public class LevelManager : MonoBehaviour
    {
        #region Singleton
        private static LevelManager _instance;
        public static LevelManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<LevelManager>();
                    if (_instance == null)
                    {
                        _instance = new GameObject("LevelManager instance", typeof(LevelManager)).GetComponent<LevelManager>();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Variables
        public float LivesLeft;
        private int _currentLevelIndex = -1;

        [SerializeField] private List<SceneCollection> _levelCollections;
        #endregion


        #region Getters and Setters
        #endregion


        #region Unity Calls

        #endregion


        #region Functions
        public void LoadLevelAtCurrentIndex()
        {
            var level = _levelCollections[_currentLevelIndex];

            SceneCollectionHandler.LoadSceneCollection(level);
        }

        public void LoadLevel(int index)
        {
            _currentLevelIndex = index;

            LoadLevelAtCurrentIndex();
        }

        public void LoadNextLevel()
        {
            _currentLevelIndex++;

            LoadLevelAtCurrentIndex();
        }
        #endregion
    }
}