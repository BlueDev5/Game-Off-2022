using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.Scenes.SceneManagement
{
    [CreateAssetMenu(fileName = "New Scene Manger Config", menuName = "Scenes/Scene Manager Config", order = 1)]
    public class SceneManagerConfig : ScriptableObject
    {
        public List<SceneCollection> SceneCollections;


        public int StartingSceneIndex = 0;
    }
}