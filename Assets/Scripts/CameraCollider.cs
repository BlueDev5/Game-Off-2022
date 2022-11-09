using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    [SerializeField] Transform cameraTarget;
    public Transform CameraTarget { get { return cameraTarget; } }
    [SerializeField] bool cameraOnLevel;
    public bool CameraOnLevel { get { return cameraOnLevel; } }
}
