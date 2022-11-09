using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    bool draggingLevel;
    public bool DraggingLevel { get { return draggingLevel; }}
    Transform cameraTarget;
    public class CameraTargetArgs
    {
        public Transform cameraTarget;
    }
    public event EventHandler<CameraTargetArgs> OnCameraTargetChanged;
    public static CameraController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void CheckCameraTarget(CameraCollider cameraCollider)
    {
        if (draggingLevel != cameraCollider.CameraOnLevel)
        {
            SetCameraTarget(cameraCollider.CameraTarget);
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

    public void SetCameraMode(bool draggingLevel)
    {
        this.draggingLevel = draggingLevel;
    }
}
