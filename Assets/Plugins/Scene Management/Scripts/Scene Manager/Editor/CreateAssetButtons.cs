
using System.Collections.Generic;
using System.IO;
using GameSystems.Scenes.SceneManagement;
using UnityEditor;
using UnityEngine;

public static class CreateAssetButtons
{

    [MenuItem("Assets/Create/Scenes/New Scene Collection")]
    public static void CreateSceneCollection()
    {
        string[] sceneAssetGUIDs = Selection.assetGUIDs;

        if (sceneAssetGUIDs.Length == 0) return;

        List<SceneReference> sceneReferences = new List<SceneReference>();

        foreach (var sceneGUID in sceneAssetGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(sceneGUID);

            if (!assetPath.EndsWith(".unity")) continue;

            sceneReferences.Add(new SceneReference(assetPath, false));
        }

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        var asset = ScriptableObject.CreateInstance<SceneCollection>();
        asset.SceneReferences = sceneReferences.ToArray();
        AssetDatabase.CreateAsset(asset: asset, path: path + "Scene Collection Name.asset");

        var sceneSettings = SceneSettings.GetOrCreateSettings();
        sceneSettings.sceneManagerConfig.SceneCollections.Add(asset);
    }
}