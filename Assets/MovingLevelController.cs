using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLevelController : MonoBehaviour
{
    Transform currentLevelDragging;
    private void Update()
    {
        MovingLevel pointerOnMovingLevel = null;
        Vector3 mousePosition = MouseWorldPosition();
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector3.forward);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.TryGetComponent(out MovingLevel movingLevel))
            {
                pointerOnMovingLevel = movingLevel;
            }
        }

        if (pointerOnMovingLevel != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                currentLevelDragging = pointerOnMovingLevel.transform;
                CameraController.Instance.SetCameraMode(true);
            }
        }

        if (currentLevelDragging)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                currentLevelDragging = null;
                CameraController.Instance.SetCameraMode(false);

            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                //TODO: Drag level to mouse position
                currentLevelDragging.position = MouseWorldPosition();
            }
        }
    }

    Vector3 MouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Transform camTrans = Camera.main.transform;
        float dist = Vector3.Dot(transform.position - camTrans.position, camTrans.forward);
        mousePosition.z = dist;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
