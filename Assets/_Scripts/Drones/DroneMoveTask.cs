using Assets._Scripts.Core.Task;
using Assets._Scripts.Managers;
using Assets._Scripts.Map;
using Assets._Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.Drones
{
    public class DroneMoveTask : Task
    {
        private Drone _currentDrone = null;
        private readonly MapTile _tile;

        public override bool Valid => throw new NotImplementedException();

        public DroneMoveTask(MapTile tile, Drone drone)
        {
            this._tile = tile;
            _currentDrone = drone;
            EventManager.Instance
                .StartListening(nameof(EventName.DronMovementComplete),OnMoveFinished);
        }

        public override void Execute()
        {
            Started = true;
            
            _currentDrone.Move(_tile);
        }

        private void OnMoveFinished(Dictionary<string, object> message)
        {
            if ((int)message["id"] == _currentDrone.Id)
            {
                Debug.Log("DroneMoveTask finished: " + _currentDrone.Id);

                _currentDrone = null;
                this._isFinished = true;
                EventManager.Instance
                .StopListening(nameof(EventName.DronMovementComplete), OnMoveFinished);
            }
        }

        public override bool IsFinished()
        {
            return _isFinished;
        }

        public override void Initialise()
        {            
            if (_currentDrone == null)
            {
                _currentDrone = DroneManager.Instance.GetIdleDrone(true);
                if (_currentDrone != null)
                {
                    this.IsInitialised = true;
                }
            }  
            else
            {
                this.IsInitialised = true;
            }
        }
    }
}
