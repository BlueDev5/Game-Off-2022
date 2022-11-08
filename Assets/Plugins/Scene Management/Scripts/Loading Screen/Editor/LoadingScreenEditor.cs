using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystems.Scenes.LoadingScreen.Editors
{
    [CustomEditor(typeof(LoadingScreen))]
    public class LoadingScreenEditor : Editor
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;
        private VisualElement _rootElement;
        private LoadingScreen _target;

        private void OnEnable()
        {
            _target = target as LoadingScreen;
            _rootElement = new VisualElement();


        }

        public override VisualElement CreateInspectorGUI()
        {
            var root = _rootElement;
            root.Clear();

            _visualTreeAsset.CloneTree(root);

            BindProperties(root);

            Debug.Log("background value: " + _target.UseBackgrounds);

            root.Q<VisualElement>("RandomTips").style.display = _target.ShowRandomTips ? DisplayStyle.Flex : DisplayStyle.None;
            root.Q<VisualElement>("Backgrounds").style.display = _target.UseBackgrounds ? DisplayStyle.Flex : DisplayStyle.None;

            return root;
        }

        private void BindProperties(VisualElement root)
        {
            var showBackgroundField = root.Q<VisualElement>("UseBackgroundToggle").Q<Toggle>();
            showBackgroundField.BindProperty(serializedObject.FindProperty("_useBackgrounds"));
            showBackgroundField.RegisterCallback<ChangeEvent<bool>>((evt) =>
            {
                Toggle target = evt.target as Toggle;
                root.Q<VisualElement>("Backgrounds").style.display = _target.UseBackgrounds ? DisplayStyle.Flex : DisplayStyle.None;
            });

            var timePerBackgroundField = root.Q<FloatField>("TimePerBackground");
            timePerBackgroundField.BindProperty(serializedObject.FindProperty("_timePerBackground"));

            var backgroundFadeTimeField = root.Q<FloatField>("BackgroundFadeDuration");
            backgroundFadeTimeField.BindProperty(serializedObject.FindProperty("_backgroundFadeDuration"));

            var progressTextFormatField = root.Q<TextField>("ProgressTextFormat");
            progressTextFormatField.BindProperty(serializedObject.FindProperty("_textFormat"));

            var progressCircleField = root.Q<EnumField>("ProgressCircleType");
            progressCircleField.Init(GameSystems.Scenes.LoadingScreen.ProgressCircleType.Filled);
            progressCircleField.BindProperty(serializedObject.FindProperty("_progressCircleType"));

            var progressCircleSpeedField = root.Q<Slider>("ProgressCircleSpeed");
            progressCircleSpeedField.BindProperty(serializedObject.FindProperty("_progressCircleSpeed"));

            var showRandomTipsField = root.Q<VisualElement>("ShowRandomTips").Q<Toggle>();
            showRandomTipsField.BindProperty(serializedObject.FindProperty("_showRandomTips"));
            showRandomTipsField.RegisterCallback<ChangeEvent<bool>>((evt) =>
            {
                Toggle target = evt.target as Toggle;
                root.Q<VisualElement>("RandomTips").style.display = _target.ShowRandomTips ? DisplayStyle.Flex : DisplayStyle.None;
            });

            var timePerTipField = root.Q<FloatField>("TimePerTip");
            timePerTipField.BindProperty(serializedObject.FindProperty("_timePerTip"));

            var tipFadeDurationField = root.Q<FloatField>("TipFadeDuration");
            tipFadeDurationField.BindProperty(serializedObject.FindProperty("_tipFadeDuration"));

            var showSceneDescriptionField = root.Q<VisualElement>("ShowSceneDescription").Q<Toggle>();
            showSceneDescriptionField.BindProperty(serializedObject.FindProperty("_showScreenDescription"));
        }
    }
}