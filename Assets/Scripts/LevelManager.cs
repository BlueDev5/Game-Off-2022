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
        [SerializeField] private SceneCollection _winScreenCollection;

        [SerializeField] private List<SceneCollection> _levelCollections;
        [SerializeField] private GameObject _restartLevelButton;
        #endregion


        #region Getters and Setters
        #endregion


        #region Unity Calls
        void Update()
        {
            if (GameplayModeManager.Instance.GamePlayMode == GamePlayMode.HomeMenu)
            {
                _currentLevelIndex = -1;
                _restartLevelButton.SetActive(false);
            }
            else if (GameplayModeManager.Instance.GamePlayMode != GamePlayMode.Dead)
            {
                _restartLevelButton.SetActive(true);
            }
        }
        #endregion


        #region Functions
        public void ReloadLevel()
        {
            SceneCollectionHandler.UnloadCurrentLoadedCollection();
            LoadLevelAtCurrentIndex();
        }

        public void LoadLevelAtCurrentIndex()
        {
            if (_currentLevelIndex >= _levelCollections.Count)
            {
                SceneCollectionHandler.LoadSceneCollection(_winScreenCollection);
                _currentLevelIndex = -1;
                return;
            }

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