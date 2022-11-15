using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Portals
{
    public class Portal : MonoBehaviour
    {
        bool portalUsed;
        [SerializeField] Portal connectingPortal;
        public Portal ConnectingPortal { get { return connectingPortal; } }
        [SerializeField] Image _portalImage;

        public void SetPortalColor(Color portalColor)
        {
            _portalImage.color = portalColor;
        }

        void UserUsePortal(Transform portalUser)
        {
            portalUser.position = new Vector3(connectingPortal.transform.position.x, connectingPortal.transform.position.y, portalUser.transform.position.z);
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

        void Awake()
        {
            PortalManager.Instance.AddPortal(this);
        }
    }
}