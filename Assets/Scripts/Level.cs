using UnityEngine;


namespace Game.Levels
{
    public class Level : MonoBehaviour
    {
        #region Variables
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private Transform _playerStartPosition;
        #endregion


        #region Getters and Setters

        #endregion


        #region Unity Calls
        void Awake()
        {
            var player = GameObject.FindObjectOfType<Player.PlayerController>();
            _cameraController.WalkingTarget = player.transform;
            player.transform.position = _playerStartPosition.position;
            GameplayModeManager.Instance.GamePlayMode = GamePlayMode.Walking;
        }
        #endregion


        #region Functions

        #endregion
    }
}