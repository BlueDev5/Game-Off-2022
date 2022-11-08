using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLevel : MonoBehaviour
{
    Collider2D m_Collider;
    Vector3 offset, screenPoint;
    bool cursorOnCollider;
    private void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        cursorOnCollider = false;
        Vector3 mousePosition = MouseWorldPosition();
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector3.forward);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == m_Collider)
            {
                cursorOnCollider = true;
            }
        }
    }

    void OnMouseDown()
    {
        if (cursorOnCollider)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
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