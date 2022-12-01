using Game.Levels;
using GameSystems.Scenes.SceneManagement;
using TMPro;
using UnityEngine;


namespace Game.UI
{
    public class WinScreen : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private SceneCollection _homeScreenCollection;
        #endregion


        #region Getters and Setters

        #endregion


        #region Unity Calls
        private void Awake()
        {
            float minutes = Mathf.Floor(GameplayModeManager.Instance.TimeTaken / 60);
            float seconds = Mathf.RoundToInt(GameplayModeManager.Instance.TimeTaken % 60);

            var text = "";

            if (minutes < 10)
            {
                text += "0" + minutes.ToString() + ":";
            }
            else
            {
                text += minutes.ToString() + ":";
            }

            if (seconds < 10)
            {
                text += "0" + Mathf.RoundToInt(seconds).ToString();
            }
            else
            {
                text += Mathf.RoundToInt(seconds).ToString();
            }

            _timeText.text = "Time Taken: " + text;
        }

        public void QuitGame()
        {
            LevelManager.Instance.LivesLeft = 3;
            SceneCollectionHandler.LoadSceneCollection(_homeScreenCollection);
        }
        #endregion


        #region Functions

        #endregion
    }
}