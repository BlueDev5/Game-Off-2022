using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float dampTime;
    private Vector3 velocity = Vector3.zero;
    private float zPosition = -15f;
    [SerializeField] private Collider2D _levelFocusCollider;

    private void Start()
    {
        CameraController.Instance.OnCameraTargetChanged += CameraController_OnSetCameraTarget;
    }

    private void OnDestroy()
    {
        CameraController.Instance.OnCameraTargetChanged -= CameraController_OnSetCameraTarget;
    }

    private void CameraController_OnSetCameraTarget(object sender, CameraController.CameraTargetArgs e)
    {
        target = e.cameraTarget;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        if (GameplayModeManager.Instance.m_GameplayMode == GameplayMode.Editing)
        {
            zPosition = -20;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, 0, zPosition), ref velocity, dampTime);
        }
        else if (GameplayModeManager.Instance.m_GameplayMode == GameplayMode.Walking)
        {
            zPosition = -15;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x, target.position.y, zPosition), ref velocity, dampTime);
        }

    }
}
