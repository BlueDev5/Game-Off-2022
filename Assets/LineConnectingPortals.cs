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
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
    }

    private void Update()
    {
        lineRenderer.enabled = portal.ConnectingPortal != null;

        if (!lineRenderer.enabled)
        {
            return;
        }

        Transform[] portals = new Transform[2];
        portals[0] = transform;
        portals[1] = portal.ConnectingPortal.transform;

        lineRenderer.SetPosition(0, portals[0].position);
        lineRenderer.SetPosition(1, portals[1].position);
    }
}
