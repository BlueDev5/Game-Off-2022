using System;
using System.Collections.Generic;
using UnityEngine;


namespace GameSystems.Scenes.LoadingScreen
{
    [Serializable]
    public class LoadingScreenData
    {
        #region Variables
        /// <summary>
        /// The name of the game.
        /// </summary>
        public string GameName;

        /// <summary>
        /// The description of the currently loading screen.
        /// </summary>
        [TextArea]
        public string SceneDescription;

        /// <summary>
        /// The list of all the backgrounds that will be displayed on the background.
        /// </summary>
        public List<Sprite> Backgrounds;

        /// <summary>
        /// The randomTips to be shown randomly.
        /// </summary>
        public List<string> RandomTips;
        #endregion


        #region Getters and Setters

        #endregion


        #region Functions

        #endregion


        #region Constructors

        #endregion
    }
}