using Assets._Scripts.Map;
using Assets._Scripts.Scriptables;
using System.Collections;
using UnityEngine;

namespace Assets._Scripts.Drones
{
    public class Drone : MonoBehaviour
    {
        private DroneMovement _droneMovement;

        private bool _isIdle = true;

        public bool IsIdle => _isIdle;
        
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Awake()
        {
            _droneMovement = GetComponent<DroneMovement>();
        }

        public void Init(ScriptableDrone drone)
        { 
            _droneMovement.Init(drone.MaxSpeed, drone.MaxHeight);
        }

        public void Move(MapTile tile)
        {
            _droneMovement.Move(new Vector3(tile.transform.position.x, 0, tile.transform.position.z));
        }
    }
}