using Assets._Scripts.Map;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Map;
using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class SelectionManager : Singleton<SelectionManager>
    {
        private Camera _mainCamera;

        private MapTile _currentMapTile;
        
        private void Awake()
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }

            InputManager.Instance.OnRightMouseClicked += OnRightMouseClicked;
            InputManager.Instance.OnLeftMouseClicked += OnLeftMouseCliecked;
        }

        private void OnLeftMouseCliecked(Vector3 position)
        {
            _currentMapTile = GetHexInPosition(position);            

            if (_currentMapTile == null)
            {
                return;
            }

            OnTileSelected();
        }

        private void OnTileSelected()
        {
            MapManager.Instance.SelectTile(_currentMapTile.TileData);

            UIManager.Instance.ResetUI();
            UIManager.Instance.UpdatePanels();
        }


        private void OnRightMouseClicked(Vector3 position)
        {
            var hex = GetHexInPosition(position);

            if (hex == null)
            {
                return;
            }

            TaskManager.Instance.AddTask(new ExploreTask(hex));
            //MapManager.Instance.ExploreTile(hex.TileData);
        }

        private MapTile GetHexInPosition(Vector3 mousePosition)
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                return hit.collider.gameObject.GetComponent<MapTile>();
            }

            return null;
        }

    }
}
