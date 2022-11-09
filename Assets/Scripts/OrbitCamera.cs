using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    Transform target;
    [SerializeField] float moveSpeed;
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

        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
