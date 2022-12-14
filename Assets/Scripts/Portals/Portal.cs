using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

namespace Game.Portals
{
    public class Portal : MonoBehaviour
    {
        bool portalUsed;
        public Portal ConnectingPortal;
        [SerializeField] SpriteRenderer _portalImage;

        public Color PortalColor
        {
            get => _portalImage.color;
        }

        public void SetPortalColor(Color portalColor)
        {
            _portalImage.color = portalColor;
        }

        void UserUsePortal(Transform portalUser)
        {
            if (ConnectingPortal == null) return;

            portalUser.position = new Vector3(ConnectingPortal.transform.position.x, ConnectingPortal.transform.position.y, portalUser.transform.position.z);
            ConnectingPortal.UsePortal();

            //plays the portal sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX_use_portal", GetComponent<Transform>().position);

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

            if (collision.CompareTag("Portal User"))
            {
                print(collision.transform.name);
                UserUsePortal(collision.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerController>())
            {
                portalUsed = false;
            }
        }

        void Start()
        {
            PortalManager.Instance.AddPortal(this);
        }
    }
}