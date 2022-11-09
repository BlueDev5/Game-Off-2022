using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    bool portalUsed;
    [SerializeField] Portal connectingPortal;
    public Portal ConnectingPortal { get { return connectingPortal; } }
    void UserUsePortal(Transform portalUser)
    {
        portalUser.position = connectingPortal.transform.position;
        connectingPortal.UsePortal();
    }

    public void UsePortal()
    {
        portalUsed = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (portalUsed)
        {
            return;
        }

        if (collision.TryGetComponent(out PlayerController playerController))
        {
            UserUsePortal(playerController.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            portalUsed = false;
        }
    }
}
