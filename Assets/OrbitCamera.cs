using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField, Range(1f, 20f)] float distance = 5f;
    public void SetCameraTarget(Transform cameraTarget)
    {
        target = cameraTarget;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 focusPoint = target.position;
        Vector3 lookDirection = transform.forward;
        transform.localPosition = focusPoint - lookDirection * distance;
        transform.localPosition += Vector3.up * 1.25f;
    }
}
