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
        private readonly bool _isSubtask;
        private readonly MapTile _tile;

        public override bool Valid => throw new NotImplementedException();

        public DroneMoveTask(MapTile tile)
        {
            this._tile = tile;            
            TmpEventManager.Instance
                .StartListening(nameof(EventName.DronMovementComplete), OnMoveFinished);
        }

        public DroneMoveTask(MapTile tile, Drone drone, bool isSubtask = true)
        {
            this._tile = tile;
            _currentDrone = drone;
            this._isSubtask = isSubtask;
            TmpEventManager.Instance
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
                if(_isSubtask == false)
                {
                    _currentDrone.SetIdle();
                }

                _currentDrone = null;
                this._isFinished = true;
                TmpEventManager.Instance
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
