// Create a new type of Settings Asset.
using GameSystems.Scenes.SceneManagement;
using UnityEditor;
using UnityEngine;

class SceneSettings : ScriptableObject
{
    public const string k_MyCustomSettingsPath = "Assets/Editor/SceneSettings.asset";

    public SceneReference scene;
    public SceneManagerConfig sceneManagerConfig;

    internal static SceneSettings GetOrCreateSettings()
    {
        var settings = AssetDatabase.LoadAssetAtPath<SceneSettings>(k_MyCustomSettingsPath);
        if (settings == null)
        {
            settings = ScriptableObject.CreateInstance<SceneSettings>();
            settings.scene = null;
            settings.sceneManagerConfig = null;
            AssetDatabase.CreateAsset(settings, k_MyCustomSettingsPath);
            AssetDatabase.SaveAssets();
        }
        return settings;
    }

    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(GetOrCreateSettings());
    }
}