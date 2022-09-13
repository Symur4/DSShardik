using Assets._Scripts.Drones;
using Assets._Scripts.Map;
using Assets.Scripts.Core;
using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class DroneManager : Singleton<DroneManager>
    {
        [SerializeField]
        private GameObject DroneContainer;

        private List<Drone> _drones = new List<Drone>();

        private void Awake()
        {
            InputManager.Instance.OnKeyboardAction += OnKeyboardAction;
        }

        private void OnKeyboardAction(KeyCode code)
        {
            if(code == KeyCode.M)
            {
                if (MapManager.Instance.SelectedTile != null)
                {
                    var drone = _drones.Where(w => w.IsIdle).FirstOrDefault();
                    drone.Move(MapManager.Instance.SelectedTile);
                }
            }
        }

        public void AddDrone(MapTile tile)
        {
            var droneResource = ResourceCore.Instance
               .DroneList               
               .FirstOrDefault();

            var spawned = Instantiate(droneResource.Prefab
               , new Vector3(tile.transform.position.x, 2, tile.transform.position.z)
               , Quaternion.identity
               , DroneContainer.transform);

            var drone = spawned.GetComponent<Drone>();
            if (drone != null)
            {
                drone.Init(droneResource);
                _drones.Add(drone);                
            }

        }
    }
}