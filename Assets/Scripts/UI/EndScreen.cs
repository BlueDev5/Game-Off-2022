using TMPro;
using UnityEngine;
using Game.Levels;
using GameSystems.Scenes.SceneManagement;

namespace Game.UI
{
    public class EndScreen : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Transform _deductLifeTextPosition;
        [SerializeField] private TextMeshProUGUI _livesLeftText;
        [SerializeField] private TextMeshProUGUI _deductLifePrefab;
        [SerializeField] private float _ySpeed;
        [SerializeField] private float _disappearTime;
        [SerializeField] private SceneCollection _homeScreenCollection;
        [SerializeField] private GameObject _restartLevelButton;

        private TextMeshProUGUI _deductText;
        #endregion


        #region Getters and Setters

        #endregion


        #region Unity Calls
        void Awake()
        {
            _deductText = Instantiate<TextMeshProUGUI>(_deductLifePrefab, _deductLifeTextPosition);
            _livesLeftText.text = "Lives Left: " + LevelManager.Instance.LivesLeft;
            LevelManager.Instance.LivesLeft--;

            if (LevelManager.Instance.LivesLeft == 0)
            {
                _restartLevelButton.SetActive(false);
            }
        }

        void Update()
        {
            if (_deductText == null) return;

            _deductText.transform.position += new Vector3(0, _ySpeed * Time.deltaTime);

            _deductText.alpha -= Time.deltaTime / _disappearTime;
            if (_deductText.alpha <= 0)
            {
                Destroy(_deductText);
                _livesLeftText.text = "Lives Left: " + LevelManager.Instance.LivesLeft;
            }
        }
        #endregion


        #region Functions
        public void RestartLevel()
        {
            Levels.LevelManager.Instance.LoadLevelAtCurrentIndex();
        }

        public void QuitGame()
        {
            LevelManager.Instance.LivesLeft = 3;
            SceneCollectionHandler.LoadSceneCollection(_homeScreenCollection);
        }
        #endregion
    }
}