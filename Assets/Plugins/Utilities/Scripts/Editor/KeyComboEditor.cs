using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;


namespace UtilsEditor
{
    [CustomPropertyDrawer(typeof(KeyCombo))]
    public class KeyComboEditor : PropertyDrawer
    {
        private const int LINE_HEIGHT = 10;
        private VisualElement _rootElement;
        private VisualTreeAsset _visualTreeAsset;

        private void OnEnable()
        {
            _rootElement = new VisualElement();
            ReloadVisualTree();
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (_rootElement == null) _rootElement = new VisualElement();
            if (_visualTreeAsset == null) ReloadVisualTree();

            var container = _rootElement;
            container.Clear();

            _visualTreeAsset.CloneTree(container);

            var enumField = container.Q<EnumField>("Key");
            var altToggleField = container.Q<Toggle>("Alt");
            var ctrlToggleField = container.Q<Toggle>("Ctrl");
            var shiftToggleField = container.Q<Toggle>("Shift");
            var label = container.Q<Label>("Label");

            enumField.BindProperty(property.FindPropertyRelative("_key"));
            altToggleField.BindProperty(property.FindPropertyRelative("_alt"));
            ctrlToggleField.BindProperty(property.FindPropertyRelative("_ctrl"));
            shiftToggleField.BindProperty(property.FindPropertyRelative("_shift"));
            label.text = property.displayName;

            return container;
        }

        private void ReloadVisualTree()
        {
            _visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Plugins/Utilities/Scripts/Editor/KeyCombo.uxml");
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Change Label width and cache it to restore later.
            float labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = position.width / 4;

            position = EditorGUI.PrefixLabel(position, label);

            // Cache all the serializedProperties
            var keyProp = property.FindPropertyRelative("_key");
            var altProp = property.FindPropertyRelative("_alt");
            var ctrlProp = property.FindPropertyRelative("_ctrl");
            var shiftProp = property.FindPropertyRelative("_shift");

            // Calculate the Rects to draw.
            var toggleWidth = position.width / 6;
            var keyRect = new Rect(position.x, position.y, position.width / 2, LINE_HEIGHT);
            var altRect = new Rect(position.x + keyRect.width + 5, position.y + 3, toggleWidth, LINE_HEIGHT);
            var ctrlRect = new Rect(position.x + keyRect.width + toggleWidth, position.y + 3, toggleWidth, LINE_HEIGHT);
            var shiftRect = new Rect(position.x + keyRect.width + toggleWidth * 2, position.y + 3, toggleWidth, LINE_HEIGHT);

            EditorGUI.PropertyField(keyRect, keyProp, new GUIContent(""));

            EditorGUIUtility.labelWidth = 50;                       // Edit Label Width once again for toggles.

            altProp.boolValue = EditorGUI.ToggleLeft(altRect, "alt", altProp.boolValue);
            ctrlProp.boolValue = EditorGUI.ToggleLeft(ctrlRect, "ctrl", ctrlProp.boolValue);
            shiftProp.boolValue = EditorGUI.ToggleLeft(shiftRect, "shift", shiftProp.boolValue);

            //Restore label Width.
            EditorGUIUtility.labelWidth = labelWidth;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return LINE_HEIGHT + 8;
        }
    }
}