using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MovingLevel : MonoBehaviour
{
    [SerializeField] Transform parentTransform;
    public Transform ParentTransform { get { return parentTransform; } }
    Collider2D m_Collider;
    Vector3 offset, screenPoint;
    bool cursorOnCollider, dragging;
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

        if (cursorOnCollider)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, screenPoint.z));
                dragging = true;
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && dragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            parentTransform.position = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            dragging = false;
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