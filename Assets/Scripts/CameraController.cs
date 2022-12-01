using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Singleton
    private static CameraController _instance;
    public static CameraController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CameraController>();
                if (_instance == null)
                {
                    _instance = new GameObject("CameraController instance", typeof(CameraController)).GetComponent<CameraController>();
                }
            }
            return _instance;
        }
    }
    #endregion

    Transform cameraTarget;
    public class CameraTargetArgs
    {
        public Transform cameraTarget;
    }
    public event EventHandler<CameraTargetArgs> OnCameraTargetChanged;

    public Transform WalkingTarget;
    [SerializeField] private Transform _editingTarget;

    public void CheckCameraTarget()
    {
        if (GameplayModeManager.Instance.GamePlayMode == GamePlayMode.Walking)
        {
            SetCameraTarget(WalkingTarget);
        }
        else if (GameplayModeManager.Instance.GamePlayMode == GamePlayMode.Editing)
        {
            SetCameraTarget(_editingTarget);
        }
    }

    void SetCameraTarget(Transform cameraTarget)
    {
        if (this.cameraTarget != cameraTarget)
        {
            OnCameraTargetChanged?.Invoke(this, new CameraTargetArgs()
            {
                cameraTarget = cameraTarget
            });
        }
        this.cameraTarget = cameraTarget;
    }
}
