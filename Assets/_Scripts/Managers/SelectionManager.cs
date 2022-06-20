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
        
        private void Awake()
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }

            InputManager.Instance.OnRightMouseClicked += OnRightMouseClicked;
        }

        private void OnRightMouseClicked(Vector3 position)
        {
            var hex = GetHexInPosition(position);

            Debug.Log(hex.TileData);

            if (hex == null)
            {
                return;
            }

            MapManager.Instance.ExploreTile(hex.TileData);
        }

        private TileBase GetHexInPosition(Vector3 mousePosition)
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                return hit.collider.gameObject.GetComponent<TileBase>();
            }

            return null;
        }

    }
}
