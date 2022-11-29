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
        [SerializeField] private Color _unconnectedPortalColor;
        [SerializeField] private Color _selectedColor;

        [SerializeField] private List<Color> _connectionColors;

        [SerializeField] private GameObject _deleteConnectionButton;


        private Portal _selectedPortal;
        private Color _selectedPortalColor;
        #endregion


        #region Getters and Setters
        #endregion


        #region Unity Calls
        void Update()
        {
            if (GameplayModeManager.Instance.m_GameplayMode != GameplayMode.Editing) return;

            if (Input.GetMouseButtonDown(0))
            {
                Portal portalUnderMouse = null;
                var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                foreach (var portal in _allPortals)
                {
                    var portalBounds = portal.GetComponent<Collider2D>().bounds;
                    if (portalBounds.IntersectRay(mouseRay))
                    {
                        portalUnderMouse = portal;
                    }
                }

                if (portalUnderMouse == null) return;

                PortalSelected(portalUnderMouse);
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

        /// Function for checking if another connection already exists for the specified portal.
        /// </summary>
        /// <param name="portal"> The portal to check </param>
        /// <returns> True if a connection exists for the passed portal. Else returns false. </returns>
        public bool AnotherConnectionExists(Portal portal)
        {
            foreach (var connection in _portalConnections)
            {
                if (connection.portal1 == portal || connection.portal2 == portal) return true;
            }

            return false;
        }

        private void PortalSelected(Portal portal)
        {
            //play portal select sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX_select_portal");

            if (_selectedPortal == null)
            {
                _selectedPortal = portal;
                _selectedPortalColor = _selectedPortal.PortalColor;
                _selectedPortal.SetPortalColor(_selectedColor);

                if (_selectedPortal.ConnectingPortal != null)
                {
                    _deleteConnectionButton.SetActive(true);
                }

                return;
            }

            if (_selectedPortal.gameObject.GetInstanceID() == portal.gameObject.GetInstanceID())
            {
                _selectedPortal.SetPortalColor(_selectedPortalColor);
                _selectedPortal = null;
                _selectedPortalColor = _unconnectedPortalColor;

                return;
            }

            if (_selectedPortal.ConnectingPortal != null || portal.ConnectingPortal != null)
            {
                _selectedPortal.SetPortalColor(_selectedPortalColor);
                _selectedPortal = null;
                _selectedPortalColor = _unconnectedPortalColor;

                return;
            }

            AddPortalConnection(_selectedPortal, portal);
            _selectedPortal = null;
            _selectedPortalColor = _unconnectedPortalColor;
        }

        /// <summary>
        /// Add a portal connection to the list of connections. 
        /// </summary>
        /// <param name="portalFrom"></param>
        /// <param name="portalTo"></param>
        /// <summary>
        private void AddPortalConnection(Portal portalFrom, Portal portalTo)
        {
            if (AnotherConnectionExists(portalFrom) || AnotherConnectionExists(portalTo)) return;

            var portalColor = getPortalColor();

            portalFrom.SetPortalColor(portalColor);
            portalTo.SetPortalColor(portalColor);

            portalFrom.ConnectingPortal = portalTo;
            portalTo.ConnectingPortal = portalFrom;

            var connection = new PortalConnection()
            {
                portal1 = portalFrom,
                portal2 = portalTo,
                portalColor = portalColor,
            };
            _portalConnections.Add(connection);

            _selectedPortal = null;
            _selectedPortalColor = _unconnectedPortalColor;

            //play portal connection sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX_connect_portal");
        }

        private Color getPortalColor()
        {
            var possibleColors = new List<Color>(_connectionColors);

            foreach (var connection in _portalConnections)
            {
                if (possibleColors.Contains(connection.portalColor))
                {
                    possibleColors.Remove(connection.portalColor);
                }
            }

            return possibleColors[Random.Range(0, possibleColors.Count)];
        }

        public void RemoveSelectedPortalConnection()
        {
            if (_selectedPortal == null) return;
            if (_selectedPortal.ConnectingPortal == null) return;

            _selectedPortal.ConnectingPortal.SetPortalColor(_unconnectedPortalColor);
            _selectedPortal.SetPortalColor(_unconnectedPortalColor);

            _selectedPortal.ConnectingPortal.ConnectingPortal = null;
            _selectedPortal.ConnectingPortal = null;

            for (int i = 0; i < _portalConnections.Count; i++)
            {
                PortalConnection connection = _portalConnections[i];

                if (connection.portal1 != _selectedPortal && connection.portal2 != _selectedPortal)
                {
                    continue;
                }

                _portalConnections.RemoveAt(i);
            }

            _selectedPortal.ConnectingPortal = null;
            _selectedPortal = null;
            _selectedPortalColor = _unconnectedPortalColor;

            //play unselect portal sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX_delete_connection");
        }
        #endregion
    }
}