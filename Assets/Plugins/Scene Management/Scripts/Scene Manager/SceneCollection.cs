using UnityEngine;

namespace GameSystems.Scenes.SceneManagement
{
    [CreateAssetMenu(fileName = "New Scene Collection", menuName = "Scenes/Scene Collection", order = 0)]
    public class SceneCollection : ScriptableObject
    {
        public string Name;
        public SceneReference[] SceneReferences;
        public int ActiveSceneIndex;

    }
}