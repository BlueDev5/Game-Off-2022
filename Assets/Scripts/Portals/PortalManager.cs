using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Utils;

namespace Game.Portals
{
    public class PortalManager : MonoBehaviour
    {
        #region Singleton
        private static PortalManager _instance;
        public static PortalManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<PortalManager>();
                    if (_instance == null)
                    {
                        _instance = new GameObject("PortalManager instance", typeof(PortalManager)).GetComponent<PortalManager>();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Variables
        private List<PortalConnection> _portalConnections = new List<PortalConnection>();
        private List<Portal> _allPortals = new List<Portal>();
        [SerializeField] private KeyCombo _togglePortalMode;
        private bool _isPortalMode;
        #endregion


        #region Getters and Setters
        #endregion


        #region Unity Calls
        void Update()
        {

            if (_togglePortalMode.IsComboDown())
            {

            }
        }
        #endregion


        #region Functions
        /// <summary>
        /// Add a portal to the list of registered portals. 
        /// </summary>
        public void AddPortal(Portal portal)
        {
            _allPortals.Add(portal);
        }

        /// <summary>
        /// Add a portal connection to the list of connections. 
        /// </summary>
        /// <param name="portalFrom"></param>
        /// <param name="portalTo"></param>
        public void AddPortalConnection(Portal portalFrom, Portal portalTo)
        {
            // Another connection already exists
            if (AnotherConnectionExists(portalFrom) || AnotherConnectionExists(portalTo))
            {
                return;
            }

            _portalConnections.Add(new PortalConnection() { portal1 = portalFrom, portal2 = portalTo });

            var portalColor = getRandomLightColor();
            portalFrom.SetPortalColor(portalColor);
            portalTo.SetPortalColor(portalColor);
        }

        /// <summary>
        /// Function for checking if another connection already exists for the specified portal.
        /// </summary>
        /// <param name="portal"> The portal to check </param>
        /// <returns> True if a connection exists for the passed portal. Else returns false. </returns>
        public bool AnotherConnectionExists(Portal portal)
        {
            foreach (var connection in _portalConnections)
            {
                if (connection.portal1 == portal) return true;
            }

            return false;
        }

        private Color getRandomLightColor()
        {
            return ColorFromHSV(Random.Range(0, 360), Random.Range(80, 100), Random.Range(0, 30));
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return new Color(255, v, t, p);
            else if (hi == 1)
                return new Color(255, q, v, p);
            else if (hi == 2)
                return new Color(255, p, v, t);
            else if (hi == 3)
                return new Color(255, p, q, v);
            else if (hi == 4)
                return new Color(255, t, p, v);
            else
                return new Color(255, v, p, q);
        }
        #endregion
    }
}