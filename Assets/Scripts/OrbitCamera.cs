using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float dampTime;
    private Vector3 velocity = Vector3.zero;
    private float zPosition = -15f;
    [SerializeField] private int _playerFocusZPos;
    [SerializeField] private int _levelFocusZPos;

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
        print(e.cameraTarget.name + "; Attached");
        target = e.cameraTarget;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        if (GameplayModeManager.Instance.GamePlayMode == GamePlayMode.Editing)
        {
            zPosition = _levelFocusZPos;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, 0, zPosition), ref velocity, dampTime * Time.timeScale);
        }
        else if (GameplayModeManager.Instance.GamePlayMode == GamePlayMode.Walking)
        {
            zPosition = _playerFocusZPos;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x, target.position.y, zPosition), ref velocity, dampTime * Time.timeScale);
        }

    }
}
