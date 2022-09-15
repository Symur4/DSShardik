using Assets._Scripts.Map;
using Assets._Scripts.Scriptables;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Scripts.Drones
{
    public class Drone : MonoBehaviour
    {
        private DroneMovement _droneMovement;

        private bool _isIdle = true;

        public bool IsIdle => _isIdle;
        
        public event Action OnMoveFinished;

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
            OnMoveFinished += MoveFinished;
        }

        private void MoveFinished()
        {
            this._isIdle = true;
            Debug.Log("Drone is idle");
        }

        public void Init(ScriptableDrone drone)
        { 
            _droneMovement.Init(drone.MaxSpeed, drone.MaxHeight);
        }

        public void Move(MapTile tile)
        {
            _droneMovement.Move(new Vector3(tile.transform.position.x, this.transform.position.y , tile.transform.position.z)
                ,OnMoveFinished);
        }

        public void SetBusy()
        {
            _isIdle = false;
        }
    }
}