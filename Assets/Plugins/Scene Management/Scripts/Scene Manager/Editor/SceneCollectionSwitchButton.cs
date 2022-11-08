using System.Collections.Generic;
using System.Linq;
using GameSystems.Scenes.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

[InitializeOnLoad]
public class SceneSwitchLeftButton
{
    private static SceneManagerConfig _config;
    private static int _selectedIndex = 0;

    static SceneSwitchLeftButton()
    {
        ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);

        _config = SceneSettings.GetOrCreateSettings().sceneManagerConfig;
    }

    static void OnToolbarGUI()
    {
        if (_config == null)
        {
            _config = SceneSettings.GetOrCreateSettings().sceneManagerConfig;
            if (_config == null) return;
        }

        GUILayout.FlexibleSpace();

        List<string> options = new List<string>();

        options.Add("BootLoader");

        foreach (var sceneCollection in _config.SceneCollections)
        {
            options.Add(sceneCollection.Name);
        }

        int tempIndex = _selectedIndex;
        _selectedIndex = EditorGUILayout.Popup(_selectedIndex, options.ToArray());

        if (_selectedIndex != tempIndex)
        {
            updateScene();
        }
    }

    private static void updateScene()
    {
        if (_selectedIndex == 0)
        {
            EditorSceneManager.OpenScene(SceneSettings.GetOrCreateSettings().scene.ScenePath);
            return;
        }

        var collection = _config.SceneCollections[_selectedIndex - 1];

        foreach (var scene in collection.SceneReferences)
        {
            EditorSceneManager.OpenScene(scene.ScenePath);
        }

    }
}