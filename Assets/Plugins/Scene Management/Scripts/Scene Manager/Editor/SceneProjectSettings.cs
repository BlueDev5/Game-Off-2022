using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameSystems.Scenes.SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// Register a SettingsProvider using IMGUI for the drawing framework:
static class MyCustomSettingsIMGUIRegister
{
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        // First parameter is the path in the Settings window.
        // Second parameter is the scope of this setting: it only appears in the Project Settings window.
        var provider = new SettingsProvider("Project/MyCustomIMGUISettings", SettingsScope.Project)
        {
            // By default the last token of the path is used as display name if no label is provided.
            label = "Scene Manager Settings",
            // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
            guiHandler = (searchContext) =>
            {
                var settings = SceneSettings.GetSerializedSettings();
                EditorGUILayout.PropertyField(settings.FindProperty("scene"), new GUIContent("BootLoader Scene"));
                EditorGUILayout.PropertyField(settings.FindProperty("sceneManagerConfig"), new GUIContent("Scene Manager Configuration"));

                settings.ApplyModifiedProperties();
            },

            // Populate the search keywords to enable smart search filtering and label highlighting:
            keywords = new HashSet<string>(new[] { "BootLoader Scene", "Scene Manager Configuration" })
        };

        return provider;
    }
}


// Create MyCustomSettingsProvider by deriving from SettingsProvider:
class MyCustomSettingsProvider : SettingsProvider
{
    private SerializedObject _CustomSettings;

    class Styles
    {
    }

    const string k_MyCustomSettingsPath = "Assets/Editor/MyCustomSettings.asset";
    public MyCustomSettingsProvider(string path, SettingsScope scope = SettingsScope.User)
        : base(path, scope) { }

    public static bool IsSettingsAvailable()
    {
        return File.Exists(k_MyCustomSettingsPath);
    }

    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        // This function is called when the user clicks on the MyCustom element in the Settings window.
        _CustomSettings = SceneSettings.GetSerializedSettings();
    }

    public override void OnGUI(string searchContext)
    {
        // Use IMGUI to display UI:
        EditorGUILayout.PropertyField(_CustomSettings.FindProperty("scene"));
        EditorGUILayout.PropertyField(_CustomSettings.FindProperty("sceneManagerConfig"), new GUIContent("Scene Manager Configuration"));

        _CustomSettings.ApplyModifiedProperties();
    }

    // Register the SettingsProvider
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        if (IsSettingsAvailable())
        {
            var provider = new MyCustomSettingsProvider("Project/MyCustomSettingsProvider", SettingsScope.Project);

            // Automatically extract all keywords from the Styles.
            provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
            return provider;
        }

        // Settings Asset doesn't exist yet; no need to display anything in the Settings window.
        return null;
    }
}