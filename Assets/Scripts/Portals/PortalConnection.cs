using UnityEngine;

namespace Game.Portals
{
    /// <summary>
    /// Struct representing a group of 2 portals that are connected. 
    /// </summary>
    [System.Serializable]
    struct PortalConnection
    {
        public Portal portal1;
        public Portal portal2;
        public Color portalColor;
    }
}