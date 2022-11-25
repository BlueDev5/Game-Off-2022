using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform cameraTarget;
    public class CameraTargetArgs
    {
        public Transform cameraTarget;
    }
    public event EventHandler<CameraTargetArgs> OnCameraTargetChanged;
    public static CameraController Instance { get; private set; }

    [SerializeField] private Transform _walkingTarget;
    [SerializeField] private Transform _editingTarget;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void CheckCameraTarget()
    {
        if (GameplayModeManager.Instance.m_GameplayMode == GameplayMode.Walking)
        {
            SetCameraTarget(_walkingTarget);
        }
        else if (GameplayModeManager.Instance.m_GameplayMode == GameplayMode.Editing)
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
