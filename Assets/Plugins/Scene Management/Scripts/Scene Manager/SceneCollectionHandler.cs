using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystems.Scenes.SceneManagement
{
    public static class SceneCollectionHandler
    {
        #region Variables
        /// <summary>
        /// The currently registered sceneCollections.
        /// </summary>
        public static SceneCollection[] SceneCollections;

        /// <summary>
        /// The scene collection Index which will be loaded on starting the game.
        /// </summary>
        public static int DefaultSceneIndex;

        /// <summary>
        /// The last loaded sceneCollection.
        /// </summary>
        private static SceneCollection _lastCollection;

        /// <summary>
        /// The current loaded sceneCollection.
        /// </summary>
        private static SceneCollection _currentCollection;

        /// <summary>
        /// The ongoing operations of loading or unloading scenes.
        /// </summary>
        private static List<AsyncOperation> _operations;

        /// <summary>
        /// Is any collection currently being loaded.
        /// </summary>
        private static bool _isLoading;

        /// <summary>
        /// Has the scene collection handler loaded.
        /// </summary>
        private static bool _hasInitialized;

        /// <summary>
        /// The instance of the initializer which will be used for invoking coroutines.
        /// </summary>
        private static SceneCollectionHandlerInitializer _instance;

        /// <summary>
        /// The current load _progress.
        /// </summary>
        private static float _progress;

        /// <summary>
        /// An event which is triggered when a scene collection starts loading.
        /// </summary>
        public static Event<int> OnLoadStart;

        /// <summary>
        /// An event which is triggered when a scene collection completes loading.
        /// </summary>
        public static Empty OnLoadComplete;
        #endregion


        #region Getters And Setters
        /// <summary>
        /// The getter for the currently loaded collection.
        /// </summary>
        public static SceneCollection CurrentCollection => _currentCollection;

        /// <summary>
        /// The getter for knowing if any sceneCollection is being loaded.
        /// </summary>
        public static bool IsLoading => _isLoading;

        /// <summary>
        /// The getter for the progress
        /// </summary>
        public static float Progress => _progress;
        #endregion


        #region Init
        /// <summary>
        /// Initialize the scene collection handler.
        /// </summary>
        public static void Initialize(int defaultSceneIndex, SceneCollection[] sceneCollections, SceneCollectionHandlerInitializer instance)
        {
            if (_hasInitialized == true) return;

            DefaultSceneIndex = defaultSceneIndex;
            SceneCollections = sceneCollections;
            _instance = instance;
            LoadSceneCollection(DefaultSceneIndex);

            _hasInitialized = true;
        }
        #endregion


        #region Load Functions
        /// <summary>
        /// Load the scene collection from its index.
        /// </summary>
        /// <param name="collectionIndex"> this is the index according to the registered scene collections list </param>
        public static void LoadSceneCollection(int collectionIndex)
        {
            _operations = new List<AsyncOperation>();
            if (collectionIndex > SceneCollections.Length || collectionIndex == -1) return;
            if (SceneCollections[collectionIndex] == null) return;

            SceneCollection collection = SceneCollections[collectionIndex];
            _lastCollection = _currentCollection;

            OnLoadStart?.Invoke(collectionIndex);

            foreach (var sceneRef in collection.SceneReferences)
            {
                LoadScene(sceneRef);
            }
            UnloadCurrentLoadedCollection(false);

            _currentCollection = collection;
            _instance.StartCoroutine(UpdateSceneLoadProgress());
        }

        /// <summary>
        /// Load the scene collection from its reference.
        /// </summary>
        /// <param name="collection"> The reference to collection being loaded. </param>
        public static void LoadSceneCollection(SceneCollection collection)
        {
            _operations = new List<AsyncOperation>();
            if (collection == null || !SceneCollections.Contains(collection)) return;

            _lastCollection = _currentCollection;

            OnLoadStart?.Invoke(SceneCollections.ToList().IndexOf(collection));

            foreach (var sceneRef in collection.SceneReferences)
            {
                LoadScene(sceneRef);
            }

            UnloadCurrentLoadedCollection(false);

            _currentCollection = collection;
            _instance.StartCoroutine(UpdateSceneLoadProgress());
        }

        /// <summary>
        /// Load the collection from its reference.
        /// </summary>
        /// <param name="collection"> The reference to collection being loaded. </param>
        /// <param name="forceUnload"> Force the unload of the currently loaded collection's scenes 
        /// even if the scene is marked persistent. </param>
        public static void LoadSceneCollection(SceneCollection collection, bool forceUnload = false)
        {
            _operations = new List<AsyncOperation>();
            if (collection == null || !SceneCollections.Contains(collection)) return;

            _lastCollection = _currentCollection;

            OnLoadStart?.Invoke(SceneCollections.ToList().IndexOf(collection));

            foreach (var sceneRef in collection.SceneReferences)
            {
                LoadScene(sceneRef);
            }

            UnloadCurrentLoadedCollection(forceUnload);

            _currentCollection = collection;
            _instance.StartCoroutine(UpdateSceneLoadProgress());
        }

        /// <summary>
        /// Helper function for loading only 1 specific scene from its scene reference.
        /// </summary>
        /// <param name="sceneRef"></param>
        private static void LoadScene(SceneReference sceneRef)
        {
            string sceneName = sceneRef.ScenePath.Split('/').Last();
            sceneName = sceneName.Substring(0, sceneName.Length - 6);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            _operations.Add(operation);
        }
        #endregion


        #region  Update
        /// <summary>
        /// Update the progress of scene collection load.
        /// </summary>
        public static IEnumerator UpdateSceneLoadProgress()
        {
            _isLoading = true;
            foreach (var operation in _operations)
            {
                while (!operation.isDone)
                {
                    _progress = 0;
                    _operations.ForEach((AsyncOperation operation) =>
                    {
                        _progress += operation.progress / _operations.Count;
                    });
                    _progress = Mathf.Clamp01(_progress / 0.9f);

                    yield return null;
                }

            }

            _isLoading = false;

            SceneReference activeSceneRef = _currentCollection.SceneReferences[_currentCollection.ActiveSceneIndex];
            Scene activeScene = SceneManager.GetSceneByPath(activeSceneRef.ScenePath);
            SceneManager.SetActiveScene(activeScene);

            OnLoadComplete?.Invoke();
        }
        #endregion


        #region Unload Functions
        /// <summary>
        /// Unload the currently loaded scene collection
        /// </summary>
        /// <param name="forceUnload"> Force the unload of the currently loaded collection's scenes 
        /// even if the scene is marked persistent. </param>
        public static void UnloadCurrentLoadedCollection(bool forceUnload = false)
        {
            if (_currentCollection == null) return;

            foreach (var sceneRef in _currentCollection.SceneReferences)
            {
                UnloadScene(sceneRef, forceUnload);
            }

            _currentCollection = null;
            _instance.StartCoroutine(UpdateSceneLoadProgress());
        }

        /// <summary>
        /// Helper function for unloading only 1 scene.
        /// </summary>
        /// <param name="sceneRef"> The reference to the scene being unloaded. </param>
        /// <param name="forceUnload"> Force the unload of the currently loaded collection's scenes 
        /// even if the scene is marked persistent. </param>
        private static void UnloadScene(SceneReference sceneRef, bool forceUnload)
        {
            if (sceneRef.IsPersistent && !forceUnload) return;

            Scene scene = SceneManager.GetSceneByPath(sceneRef.ScenePath);
            AsyncOperation operation = SceneManager.UnloadSceneAsync(scene.buildIndex);

            _operations.Add(operation);
        }
        #endregion
    }
}