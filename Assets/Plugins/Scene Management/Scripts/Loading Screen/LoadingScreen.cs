using System.Collections.Generic;
using GameSystems.Scenes.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameSystems.Scenes.LoadingScreen
{
    public class LoadingScreen : MonoBehaviour
    {
        #region  Miscellaneous or Global Variables
        /// <summary>
        /// The smooth speed used for displaying loading Circle, Progress Var and Progress Text.
        /// </summary>
        private const float PROGRESS_SMOOTH_SPEED = 0.1f;

        /// <summary>
        /// Is the loading screen currently Opened.
        /// </summary>
        private bool _isOpened = false;
        #endregion

        #region Private References
        /// <summary>
        /// The background of the loading Screen.
        /// </summary>
        private Image _background;

        /// <summary>
        /// /// The text representing the current loading progress.
        /// </summary>
        private TMP_Text _loadingText;

        /// <summary>
        /// The slider representing the progress bar.
        /// </summary>
        private Slider _progressBar;

        /// <summary>
        /// The text representing the name of the game.
        /// </summary>
        private TMP_Text _gameNameText;

        /// <summary>
        /// The text for the description of the currently loading scene.
        /// </summary>
        private TMP_Text _sceneDescriptionText;

        /// <summary>
        /// The text representing some tip for the player.
        /// </summary>
        private TMP_Text _tipText;

        /// <summary>
        /// The loading screen manager reference to access the loading screen data of the currently loaded 
        /// scene collection
        /// </summary>
        private LoadingScreenManager _loadingScreenData;

        /// <summary>
        /// The progress circle (the circle around the loading text) of the loading screen.
        /// </summary>
        private Image _progressCircle;
        #endregion


        #region Backgrounds
        [Header("Backgrounds")]

        [SerializeField] private bool _useBackgrounds;

        /// <summary>
        /// The time a background will stay on the screen.
        /// </summary>
        [SerializeField] private float _timePerBackground;

        /// <summary>
        /// The time in which a background will fade into another background.
        /// </summary>
        [SerializeField] private float _backgroundFadeDuration;
        #endregion


        #region Loading text
        [Header("Loading Text")]

        private bool _showLoadingText;
        private bool _showLoadingProgressBar;
        private bool _showLoadingCircle;

        /// <summary>
        /// The format in which the loading text is rendered.
        /// </summary>
        [SerializeField] private string _textFormat = "Loading {percent}";

        // TODO: Add fields for the progress circle.
        /// <summary>
        /// The type of the progress circle, weather filled or rotating.
        /// </summary>
        [SerializeField] private ProgressCircleType _progressCircleType;

        /// <summary>
        /// The speed at which the progress Circle rotates.
        /// </summary>
        [SerializeField] private float _progressCircleSpeed = 50;
        #endregion


        #region Random Tips
        [Header("Random Tips")]

        [SerializeField] private bool _showRandomTips;

        /// <summary>
        /// The time a tip will stay on the screen.
        /// </summary>
        [SerializeField] private float _timePerTip;

        /// <summary>
        /// The time in which a tip will fade into another tip.
        /// </summary>
        [SerializeField] private float _tipFadeDuration;
        #endregion

        /// <summary>
        /// A toggle for showing the screen Description on the loading screen.
        /// </summary>
        [SerializeField] private bool _showScreenDescription;


        #region Getters and Setters
        public bool ShowScreenDescription => _showScreenDescription;

        public bool ShowRandomTips => _showRandomTips;

        public bool UseBackgrounds => _useBackgrounds;
        #endregion


        #region Unity Calls
        private void Awake()
        {
            SetPrivateReferences();

            SceneCollectionHandler.OnLoadStart += Show;

            gameObject.SetActive(false);
        }
        #endregion


        #region Main Functions
        /// <summary>
        /// Method for grabbing and setting all the references for manipulating the loading screen.
        /// </summary>
        private void SetPrivateReferences()
        {
            _background = transform.Find("BG")?.GetComponent<Image>();
            _gameNameText = transform.Find("Game Name").GetComponent<TMP_Text>();
            _tipText = transform.Find("Tip").GetComponent<TMP_Text>();
            _sceneDescriptionText = transform.Find("Scene Description").GetComponent<TMP_Text>();
            _loadingScreenData = Resources.Load<LoadingScreenManager>("Screens Manager");

            _loadingText = transform.Find("Mask").Find("Loading").GetComponent<TMP_Text>();
            _progressBar = transform.Find("Progress").GetComponent<Slider>();
            _progressCircle = transform.Find("Mask").Find("Progress Circle").GetComponent<Image>();

            _showLoadingText = _loadingText == null ? false : true;
            _showLoadingProgressBar = _progressBar == null ? false : true;
            _showLoadingCircle = _progressCircle == null ? false : true;
        }

        /// <summary>
        /// Show the loading screen
        /// </summary>
        /// <param name="currentCollectionIndex"> the index of the current collection being loaded, 
        /// so that the data for that loading screen be loaded </param>
        public void Show(int currentCollectionIndex)
        {
            if (_isOpened) return;

            gameObject.SetActive(true);
            var currentScreenData = _loadingScreenData.Screens[currentCollectionIndex];

            _gameNameText.text = currentScreenData.GameName;

            if (_useBackgrounds)
            {
                _background.gameObject.SetActive(true);

                StartCoroutine(FadeBackgrounds(currentCollectionIndex, currentScreenData));
            }
            else if (!_useBackgrounds) _background.gameObject.SetActive(false);

            if (_showRandomTips)
            {
                _tipText.gameObject.SetActive(true);

                StartCoroutine(FadeTips(currentCollectionIndex, currentScreenData));
            }
            else if (!_showRandomTips) _tipText.gameObject.SetActive(false);

            if (_showLoadingProgressBar)
            {
                _progressBar.gameObject.SetActive(true);
                StartCoroutine(UpdateLoadingProgressBar());
            }
            else _progressBar?.gameObject.SetActive(false);

            if (_showLoadingText)
            {
                _loadingText.gameObject.SetActive(true);
                StartCoroutine(UpdateLoadingProgressText());
            }
            else _loadingText?.gameObject.SetActive(false);

            if (_showScreenDescription)
            {
                _sceneDescriptionText.gameObject.SetActive(true);
                _sceneDescriptionText.text = currentScreenData.SceneDescription;
            }
            else _sceneDescriptionText.gameObject.SetActive(false);

            if (_showLoadingCircle)
            {
                _progressCircle.gameObject.SetActive(true);

                if (_progressCircleType == ProgressCircleType.Filled)
                {
                    _progressCircle.transform.rotation = Quaternion.identity;
                    _progressCircle.type = Image.Type.Filled;
                    _progressCircle.fillAmount = 0;
                    StartCoroutine(UpdateProgressCircle());
                }
                else if (_progressCircleType == ProgressCircleType.Rotating)
                {
                    _progressCircle.transform.rotation = Quaternion.identity;
                    StartCoroutine(RotateProgressCircle());
                }
            }
            else if (!_showLoadingCircle) _progressCircle?.gameObject.SetActive(false);

            _isOpened = true;
        }


        /// <summary>
        /// Hide the loading screen.
        /// </summary>
        public void Hide()
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
            _isOpened = false;
        }
        #endregion


        #region Functions to Update Loading Screen
        private System.Collections.IEnumerator RotateProgressCircle()
        {
            while (SceneCollectionHandler.Progress <= 1)
            {
                _progressCircle.transform.Rotate(Vector3.forward, _progressCircleSpeed * 0.1f);

                yield return null;
            }
        }

        private System.Collections.IEnumerator UpdateProgressCircle()
        {
            while (SceneCollectionHandler.Progress <= 1)
            {
                _progressCircle.type = Image.Type.Filled;
                _progressCircle.fillAmount = Mathf.Lerp(_progressCircle.fillAmount, SceneCollectionHandler.Progress, PROGRESS_SMOOTH_SPEED);

                yield return null;
            }
        }

        private float _lastFrameProgress = 0f;
        private System.Collections.IEnumerator UpdateLoadingProgressText()
        {
            _loadingText.text = "00";
            while (SceneCollectionHandler.Progress <= 1)
            {
                float currentProgress = Mathf.Lerp(_lastFrameProgress, SceneCollectionHandler.Progress, PROGRESS_SMOOTH_SPEED);

                var text = _textFormat;
                text = text.Replace("{percent}", (currentProgress * 100).ToString("00"));
                text = text.Replace("{decimal}", (currentProgress * 100).ToString("0.##"));
                text = text.Replace("{progress_01}", currentProgress.ToString("0.##"));
                _loadingText.text = text;

                _lastFrameProgress = currentProgress;

                yield return null;
            }
        }

        private System.Collections.IEnumerator UpdateLoadingProgressBar()
        {
            _progressBar.value = 0;
            while (SceneCollectionHandler.Progress <= 1)
            {
                _progressBar.value = Mathf.Lerp(_progressBar.value, SceneCollectionHandler.Progress, PROGRESS_SMOOTH_SPEED);

                yield return null;
            }
        }

        private IEnumerator<WaitForSeconds> FadeTips(int currentCollectionIndex, LoadingScreenData currentScreenData)
        {
            int currentIndex = Random.Range(0, currentScreenData.RandomTips.Count);
            _tipText.text = currentScreenData.RandomTips[currentIndex];

            while (true)
            {
                yield return new WaitForSeconds(_timePerTip - (_tipFadeDuration / 2));

                var tipCanvasGroup = _tipText.gameObject.GetComponent<CanvasGroup>();
                tipCanvasGroup.LeanAlpha(0, _tipFadeDuration / 2).setOnComplete(() =>
                {
                    currentIndex = Random.Range(0, currentScreenData.RandomTips.Count);
                    _tipText.text = currentScreenData.RandomTips[currentIndex];

                    tipCanvasGroup.LeanAlpha(1, _tipFadeDuration / 2);
                });
            }
        }

        private IEnumerator<WaitForSeconds> FadeBackgrounds(int currentCollectionIndex, LoadingScreenData currentScreenData)
        {
            int currentIndex = 0;
            _background.sprite = currentScreenData.Backgrounds[currentIndex];

            while (true)
            {
                yield return new WaitForSeconds(_timePerBackground - (_backgroundFadeDuration / 2));

                var backgroundCanvasGroup = _background.gameObject.GetComponent<CanvasGroup>();
                backgroundCanvasGroup.LeanAlpha(0, _backgroundFadeDuration / 2).setOnComplete(() =>
                {
                    currentIndex++;
                    _background.sprite = currentScreenData.Backgrounds[currentIndex % currentScreenData.Backgrounds.Count];

                    backgroundCanvasGroup.LeanAlpha(1, _backgroundFadeDuration / 2);
                });
            }
        }
        #endregion
    }


    /// <summary>
    /// Enum for choosing the which type of loading circle will be presented to player
    /// </summary>
    public enum ProgressCircleType
    {
        /// <summary>
        /// The Circle will fill from 0% to 100% as the scene loading makes progress.
        /// </summary>
        Filled,

        /// <summary>
        /// The Circle will rotate continuously.
        /// </summary>
        Rotating,
    }
}