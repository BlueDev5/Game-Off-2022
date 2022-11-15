using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalConnector : MonoBehaviour
{
    public class PortalsConnectionArgs
    {
        public Portal portal1;
        public Portal portal2;
    }
    public event EventHandler<PortalsConnectionArgs> OnNewConnectionStarted;
    public event EventHandler<PortalsConnectionArgs> OnNewConnectionStarting;
    public event EventHandler<PortalsConnectionArgs> OnNewConnectionEnded;
    public event EventHandler<PortalsConnectionArgs> OnConnectionSevered;
    Portal connectingPortal;
    public static PortalConnector Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if (GameplayModeManager.Instance.m_GameplayMode != GameplayMode.Editing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Collider2D[] hits = Physics2D.OverlapPointAll(MouseWorldPosition());
            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out Portal portal))
                {
                    connectingPortal = portal;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Collider2D[] hits = Physics2D.OverlapPointAll(MouseWorldPosition());
            Portal otherPortal = null;
            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out Portal portal))
                {
                    if (portal != connectingPortal)
                    {
                        ConnectPortals(connectingPortal, portal);
                    }
                    otherPortal = portal;
                }
            }

            if (otherPortal == null)
            {
                ConnectPortals(connectingPortal, null);
            }

            connectingPortal = null;
        }

        if (connectingPortal)
        {
            OnNewConnectionStarting?.Invoke(this, new PortalsConnectionArgs()
            {
                portal1 = connectingPortal
            });
            OnConnectionSevered?.Invoke(this, new PortalsConnectionArgs()
            {
                portal1 = connectingPortal,
                portal2 = connectingPortal.ConnectingPortal
            });
        }
    }

    void ConnectPortals(Portal portal1, Portal portal2)
    {
        if (portal1 == null)
        {
            return;
        }

        portal1.ConnectToPortal(portal2);

        if (portal2 == null)
        {
            OnNewConnectionEnded?.Invoke(this, new PortalsConnectionArgs()
            {
                portal1 = portal1
            });

            return;
        }

        portal2.ConnectToPortal(portal1);

        portal2.LineConnectingPortals.SetLineRendererEnabled(false);

        OnNewConnectionStarted?.Invoke(this, new PortalsConnectionArgs()
        {
            portal1 = portal1,
            portal2 = portal2
        });
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
