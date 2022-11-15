using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Portal))]
public class LineConnectingPortals : MonoBehaviour
{
    LineRenderer lineRenderer;
    Portal portal;
    private void Awake()
    {
        portal = GetComponent<Portal>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        PortalConnector.Instance.OnNewConnectionStarted += PortalConnector_OnNewConnectionStarted;
        PortalConnector.Instance.OnNewConnectionStarting += PortalConnector_OnNewConnectionStarting;
        PortalConnector.Instance.OnNewConnectionEnded += PortalConnector_OnNewConnectionEnded;
    }

    private void OnDestroy()
    {
        PortalConnector.Instance.OnNewConnectionStarted -= PortalConnector_OnNewConnectionStarted;
        PortalConnector.Instance.OnNewConnectionStarting -= PortalConnector_OnNewConnectionStarting;
        PortalConnector.Instance.OnNewConnectionEnded -= PortalConnector_OnNewConnectionEnded;
    }

    private void PortalConnector_OnNewConnectionEnded(object sender, PortalConnector.PortalsConnectionArgs e)
    {
        SetLineRendererEnabled(false);
    }

    private void PortalConnector_OnNewConnectionStarted(object sender, PortalConnector.PortalsConnectionArgs e)
    {
        if (portal == e.portal1)
        {
            ConnectPortals();
        }
    }

    private void PortalConnector_OnNewConnectionStarting(object sender, PortalConnector.PortalsConnectionArgs e)
    {
        if (portal == e.portal1)
        {
            ConnectingPortal();
        }
    }

    void SetColor(Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    void ConnectingPortal()
    {
        SetLineRendererEnabled(true);

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, MouseWorldPosition());

        SetColor(Color.gray);
    }

    void ConnectPortals()
    {
        if (portal.ConnectingPortal == null)
        {
            return;
        }

        SetLineRendererEnabled(true);

        Transform[] portals = new Transform[2];
        portals[0] = transform;
        portals[1] = portal.ConnectingPortal.transform;

        lineRenderer.SetPosition(0, portals[0].position);
        lineRenderer.SetPosition(1, portals[1].position);

        SetColor(Color.blue);
    }

    public void SetLineRendererEnabled(bool _enabled)
    {
        lineRenderer.enabled = _enabled;
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
